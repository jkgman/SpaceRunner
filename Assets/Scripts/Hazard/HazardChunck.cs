using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardChunck : MonoBehaviour {
    private HazardGroup[] groups;
    private float startDist;
    public HazardSpawner spawner;
    private bool activeChunk;
    private void Start()
    {
        groups = GetComponentsInChildren<HazardGroup>();
        startDist = LevelController.instance.Distance;
        activeChunk = true;
    }
    public void Gen() {
        groups = GetComponentsInChildren<HazardGroup>();
        foreach(HazardGroup item in groups)
        {
            item.transform.position = transform.position + new Vector3(0,0,-item.dist);
            item.Gen();
        }
    }
    private void Update()
    {
        bool anyLeftToSpawn = false;
        foreach(HazardGroup item in groups)
        {
            Debug.Log(item.dist + " - "+(LevelController.instance.Distance - startDist));
            if(!item.active && item.dist  <= (LevelController.instance.Distance - startDist))
            {
                Debug.Log("spawn");
                spawner.Spawn(item);
                item.active = true;
            }
            if(!item.active)
            {
                anyLeftToSpawn = true;
            }
        }
        if(!anyLeftToSpawn)
        {
            spawner.NewChunck();
            Destroy(gameObject);
        }
    }
}
