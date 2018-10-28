using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider),typeof(LaneGenerator),typeof(TransitionController))]
public class LevelController : MonoBehaviour {

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
    private int currentPlanet = -1;
    public float distance;

    #region Singleton and Delegate
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

    void Start () {
        lane = GetComponent<LaneGenerator>();
        transitionController = GetComponent<TransitionController>();
        player = FindObjectOfType<PlayerHandle>();
        spawnCollider = GetComponent<BoxCollider>();
        ShufflePlanets(desiredLevelLength);
        GenerateLevelField(planetsToSpawn, spawnCollider);
        BeginPlanet(planetsInLevel[0], player);
    }

    public PlanetController GetCurrentPlanet() {
        return planetsInLevel[currentPlanet];
    }
    void Update () {
        //if(Distance > worlddistance * multiplier){
        //    EndPlanet(); 
        //}
    }

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


    void GenerateLevelField(GameObject[] planets, BoxCollider spawnArea){
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
     

    void SetupPlanet(PlanetController planet){
        lane.Generate(planet);
        hazardSpawner.Generate(planet);
    }
     

    void BeginPlanet(PlanetController planet, PlayerHandle player){
        transitionController.ToWorld(planet, player);
        currentPlanet++;
    }
    public void Begin() {
        planetsInLevel[currentPlanet].Begin();
        hazardSpawner.Begin();
        player.ActivateControl();
    }

    /*EndPlanet(){
     * Hazard.Stop();
     * planetsinLevel[currentPlanet].spawnLevelExit;
     * 
     * }
     */

    /*NextPlanet(){
     * Destroy(currentPlanet)
     * SetUpPlanet(currentplanet++)
     * BeginPlanet(currentplanet++);
     * }
     */
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

}
