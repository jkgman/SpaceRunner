using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {
    public GameObject[] spawnobjects;
    public BoxCollider box;
    public Color color;
	// Use this for initialization
	void Start () {
        for(int i = 0; i < spawnobjects.Length; i++)
        {
            float xConstraint = box.size.x;
            float yConstraint = box.size.y;
            float zConstraint = box.size.z / spawnobjects.Length;
            float x = Random.Range(0, xConstraint) - (box.size.x / 2);
            float y = Random.Range(0, yConstraint) - (box.size.y / 2);
            float z = Random.Range(0, zConstraint) - (box.size.z / 2) + zConstraint * i;
            Instantiate(spawnobjects[i],new Vector3(x,y,z) + box.transform.position, spawnobjects[i].transform.rotation,gameObject.transform);
        }
	}

    private void OnDrawGizmos()
    {
        if(box)
        {
            Gizmos.color = color;
            Gizmos.DrawCube(box.center + transform.position, box.size);
        } else
        {
            box = GetComponent<BoxCollider>();
        }
    }

}
