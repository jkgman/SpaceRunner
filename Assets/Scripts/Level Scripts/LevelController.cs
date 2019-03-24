using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// controlls the levels state
/// </summary>
[RequireComponent(typeof(BoxCollider),typeof(LaneGenerator),typeof(TransitionController))]
public class LevelController : MonoBehaviour {

    #region Variables
    public GameObject levelend;
    private PlanetController[] planetsInLevel;//planets once spawned
    private BoxCollider spawnCollider;
    private LaneGenerator lane;
    private TransitionController transitionController;
    private HazardSpawner hazardSpawner;
    private PlayerHandle player;
    private int currentPlanet;
    private float distance;
    public float initialDistBetweenWorldChange;
    [SerializeField]
    private int testLevelNumber = 0;
    [HideInInspector]
    public bool onWorld;
    public float worldChangeScaler;
    [HideInInspector]
    public int currentLane = 2;
    private bool exitspawned = false;
    [SerializeField]
    public Level[] levels;

    public float Distance
    {
        get {
            return distance;
        }

        set {
            distance = value;
        }
    }
    #endregion

    #region Singleton
    public static LevelController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of LevelController found");
            return;
        }
        instance = this;
    }
    #endregion

    #region Implementations
    void Start () {
        //ChooseLevel(GameManager.Instance.currentLevel);
        lane = GetComponent<LaneGenerator>();
        transitionController = GetComponent<TransitionController>();
        hazardSpawner = FindObjectOfType<HazardSpawner>();
        player = FindObjectOfType<PlayerHandle>();
        player.fire.Play();
        player.anim.Play("PlanetSwitchLoop");
        player.dust.Stop();
        spawnCollider = GetComponent<BoxCollider>();
        GenerateLevelField(GetLevelsPlanets(), spawnCollider);
        BeginPlanet(planetsInLevel[0], player);
        InputHandle.instance.onMovement += MovementCalc;
    }

    //Switches planets after a certain distance
    void Update()
    {
        if(Distance > initialDistBetweenWorldChange + initialDistBetweenWorldChange * currentPlanet * worldChangeScaler)
        {
            if(planetsInLevel.Length > currentPlanet + 1 && !exitspawned)
            {
                EndPlanet();
            } else if(planetsInLevel.Length == currentPlanet + 1)
            {
                EndLevel();
            }
        }
    }

    // Shows the spawning box for planets
    private void OnDrawGizmos()
    {
        if(spawnCollider)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(spawnCollider.center + transform.position, spawnCollider.size);
        } else
        {
            spawnCollider = GetComponent<BoxCollider>();
        }
    }
    #endregion

    #region public Functions
    /// <summary>
    /// returns current planet that were on or that were headed to
    /// </summary>
    /// <returns></returns>
    public PlanetController GetCurrentPlanet() {
        return planetsInLevel[currentPlanet];
    }

    /// <summary>
    /// Starts planet tracking, hazards spawning, and activates player control
    /// </summary>
    public void Begin()
    {
        planetsInLevel[currentPlanet].Begin();
        hazardSpawner.Begin();
        player.fire.Stop();
        player.anim.Play("PlanetSwitchEnd");
        player.ActivateControl();
        currentLane = 2;
        onWorld = true;
    }

    
    /// <summary>
    /// Send message that a collectable was collected to player and item manager
    /// </summary>
    /// <param name="_collectable">The type of collectable that was collected</param>
    public void SendConsumeMessage(Collectable.CollectableType _collectable)
    {
        foreach (GameObject listener in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IitemEvents>
                (listener, null, (x, y) => x.ItemCollected(_collectable));
            Debug.Log("lvlcontroller: message sent");
        }
    }

    /// <summary>
    /// Stops planet tracking hazard spawning and increase the planet were on
    /// </summary>
    public void EndPlanet()
    {
        exitspawned = true;
        hazardSpawner.Stop();
        planetsInLevel[currentPlanet].End();
    }

    /// <summary>
    /// Calls whatever fail state we want
    /// </summary>
    public void FailPlanet()
    {
        InputHandle.instance.Pause();
    }

    /// <summary>
    /// Calls to set up next planet and begin next planet after
    /// </summary>
    public void NextPlanet()
    {
        onWorld = false;
        currentPlanet++;
        exitspawned = false;
        SetupPlanet(planetsInLevel[currentPlanet]);
        BeginPlanet(planetsInLevel[currentPlanet], player);
    }
    #endregion

    #region Private Functions

    GameObject[] GetLevelsPlanets() {
        //get level number from game manager
        //return levelList[ln];
        if(GameManager.Instance)
        {
            return levels[GameManager.Instance.currentLevel].levelObject;
        }
        return levels[testLevelNumber].levelObject;
    }

    /// <summary>
    /// Picks a list of planets from the array to spawn and add to planetsInLevel. Spwaning randomly in field
    /// </summary>
    /// <param name="planets"></param>
    /// <param name="spawnArea"></param>
    void GenerateLevelField(GameObject[] planets, BoxCollider spawnArea)
    {
        planetsInLevel = new PlanetController[planets.Length];
        Vector3[] planetsPos = new Vector3[planets.Length];
        for(int i = 0; i < planets.Length; i++)
        {
            planetsInLevel[i] = Instantiate(planets[i].gameObject, Vector3.zero, Quaternion.identity).GetComponent<PlanetController>();
            float xConstraint = spawnArea.size.x;
            float yConstraint = spawnArea.size.y / planets.Length;
            float zConstraint = spawnArea.size.z / planets.Length;
            float x = Random.Range(0 + planetsInLevel[i].planetRadius, xConstraint - planetsInLevel[i].planetRadius) - (spawnArea.size.x / 2);
            float y = Random.Range(0 + planetsInLevel[i].planetRadius, yConstraint - planetsInLevel[i].planetRadius) - (spawnArea.size.y / 2) + yConstraint * i;
            float z = Random.Range(0 + planetsInLevel[i].planetRadius, zConstraint - planetsInLevel[i].planetRadius) - (spawnArea.size.z / 2) + zConstraint * i;
            planetsInLevel[i].transform.position = new Vector3(x, y, z);
        }
        SetupPlanet(planetsInLevel[0]);
    }

    

    /// <summary>
    /// tells lane and hazards to generate accourding to passed in planet
    /// </summary>
    /// <param name="planet"></param>
    void SetupPlanet(PlanetController planet)
    {
        lane.Generate(planet);
        hazardSpawner.Generate(planet);
    }

    /// <summary>
    /// call transition to planet
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="player"></param>
    void BeginPlanet(PlanetController planet, PlayerHandle player)
    {
        transitionController.ToWorld(planet, player);
    }
    public void StopPlanet() {
        planetsInLevel[currentPlanet].Stop();
    }
    public void MovementCalc(Vector2 endPos, Vector2 direction, float distance)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(onWorld)
            {
                if(direction.x < 0 && currentLane < 4)
                {
                    planetsInLevel[currentPlanet].RotateLane(lane.UnitLanePositions[currentLane], lane.UnitLanePositions[currentLane + 1]);
                    currentLane++;
                } else if(direction.x > 0 && currentLane > 0)
                {
                    planetsInLevel[currentPlanet].RotateLane(lane.UnitLanePositions[currentLane], lane.UnitLanePositions[currentLane - 1]);
                    currentLane--;
                }
            }
            //if space 
                //player move x right or left
        }
    }

    void EndLevel() {
        //stop planet rot
        EndPlanet();
        player.StopDust();
        StopPlanet();
        levelend.SetActive(true);
        //open end screen
    }
    #endregion

}
