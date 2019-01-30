using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Holds a list of Hazardgroups to spawn a chunk and
/// the functionality of spawning them
/// </summary>
public class HazardChunk : MonoBehaviour {

    #region Variables

    private HazardRow[] groups;
    private float startDist;
    private HazardSpawner spawner;
    private bool activeChunk;

    public HazardSpawner Spawner
    {
        private get {
            return spawner;
        }

        set {
            spawner = value;
        }
    }

    #endregion

    #region Implementations

    //Records starting dist, and finds the children
    private void Start()
    {
        groups = GetComponentsInChildren<HazardRow>();
        startDist = LevelController.instance.Distance;
        activeChunk = true;
    }

    //if there are elements not spawned try to spawn
    //else destroy object
    private void Update()
    {
        bool anyLeftToSpawn = false;
        foreach(HazardRow item in groups)
        {
            if(!item.active && item.dist <= (LevelController.instance.Distance - startDist))
            {
                Spawner.Spawn(item);
                item.active = true;
            }
            if(!item.active)
            {
                anyLeftToSpawn = true;
            }
        }
        if(!anyLeftToSpawn)
        {
            Spawner.NewChunck();
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Functions
    /// <summary>
    /// visually show how all hazards line up
    /// </summary>
    public void Gen() {
        groups = GetComponentsInChildren<HazardRow>();
        foreach(HazardRow item in groups)
        {
            item.transform.position = transform.position + new Vector3(0,0,-item.dist);
            item.Gen();
        }
    }
    #endregion

    #region Editor
#if UNITY_EDITOR
    //Make button to call gen
    [CustomEditor(typeof(HazardChunk))]
    public class HazardEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            HazardChunk script = (HazardChunk)target;
            if(GUILayout.Button("Generate"))
            {
                script.Gen();
            }
        }
    }
#endif
#endregion
}
