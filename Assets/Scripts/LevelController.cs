using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// controlls the game state
/// </summary>
[RequireComponent(typeof(BoxCollider),typeof(LaneGenerator),typeof(TransitionController))]
public class LevelController : MonoBehaviour {

    #region Variables
    [SerializeField]
    private int desiredLevelLength;
    [SerializeField]
    private GameObject[] planets;//pool of total planets
    private GameObject[] planetsToSpawn;//planets selected to spawn
    private PlanetController[] planetsInLevel;//planets once spawned
    private BoxCollider spawnCollider;
    private LaneGenerator lane;
    private TransitionController transitionController;
    [SerializeField]
    private HazardSpawner hazardSpawner;
    private PlayerHandle player;
    private int currentPlanet;
    private float distance;
    public float initialDistBetweenWorldChange;
    public float worldChangeScaler;

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
        lane = GetComponent<LaneGenerator>();
        transitionController = GetComponent<TransitionController>();
        player = FindObjectOfType<PlayerHandle>();
        spawnCollider = GetComponent<BoxCollider>();
        ShufflePlanets(desiredLevelLength);
        GenerateLevelField(planetsToSpawn, spawnCollider);
        BeginPlanet(planetsInLevel[0], player);
    }

    //Switches planets after a certain distance
    void Update()
    {
        if(Distance > initialDistBetweenWorldChange + initialDistBetweenWorldChange * currentPlanet * worldChangeScaler)
        {
            if(planetsInLevel.Length > currentPlanet + 1)
            {
                EndPlanet();
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
        player.ActivateControl();
    }

    
    /// <summary>
    /// Send message that a collectable was collected to player and item manager
    /// </summary>
    /// <param name="_type">The type of collectable that was collected</param>
    public void SendCollectionMessage(Collectable.CollectableType _type)
    {
        foreach (GameObject listener in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IitemEvents>
                (listener, null, (x, y) => x.ItemCollected(_type));
            Debug.Log("lvlcontroller: message sent");
        }
    }

    /// <summary>
    /// Stops planet tracking hazard spawning and increase the planet were on
    /// </summary>
    public void EndPlanet()
    {
        hazardSpawner.Stop();
        planetsInLevel[currentPlanet].End();
        currentPlanet++;
    }

    /// <summary>
    /// Calls whatever fail state we want
    /// </summary>
    public void FailPlanet()
    {
        //TODO: add a fail state
        InputHandle.instance.Pause();
    }

    /// <summary>
    /// Calls to set up next planet and begin next planet after
    /// </summary>
    public void NextPlanet()
    {
        SetupPlanet(planetsInLevel[currentPlanet]);
        BeginPlanet(planetsInLevel[currentPlanet], player);
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Shuffles our list of planets and calls generate
    /// </summary>
    void ShufflePlanets(int levelLength){
        planetsToSpawn = new GameObject[levelLength];
        planetsInLevel = new PlanetController[levelLength];
        for(int i = 0; i < planets.Length; i++){
            GameObject planet = planets[i];
            int rndPos = Random.Range(0, i);
            planets[i] = planets[rndPos];
            planets[rndPos] = planet;
        }
        for(int i = 0; i < desiredLevelLength; i++){
            planetsToSpawn[i] = planets[i];
        }
    }

    /// <summary>
    /// Picks a list of planets from the array to spawn and add to planetsInLevel. Spwaning randomly in field
    /// </summary>
    /// <param name="planets"></param>
    /// <param name="spawnArea"></param>
    void GenerateLevelField(GameObject[] planets, BoxCollider spawnArea)
    {
        //TODO: make sure the planets dont spawn closer than radius
        Vector3[] planetsPos = new Vector3[planets.Length];
        for(int i = 0; i < planets.Length; i++)
        {
            float xConstraint = spawnArea.size.x;
            float yConstraint = spawnArea.size.y / planets.Length;
            float zConstraint = spawnArea.size.z / planets.Length;
            float x = Random.Range(0, xConstraint) - (spawnArea.size.x / 2);
            float y = Random.Range(0, yConstraint) - (spawnArea.size.y / 2) + yConstraint * i;
            float z = Random.Range(0, zConstraint) - (spawnArea.size.z / 2) + zConstraint * i;
            planetsInLevel[i] = Instantiate(planets[i].gameObject, new Vector3(x, y, z), Quaternion.identity).GetComponent<PlanetController>();
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
    #endregion

}
