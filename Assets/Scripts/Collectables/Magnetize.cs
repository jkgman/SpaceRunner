using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetize : MonoBehaviour {
    float activeFor;
    bool Active;
    bool justActivated;
    BoxCollider box;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
    }
    public void Activate(float time) {
        Active = true;
        justActivated = true;
        activeFor = time;
        Collider[] cols = Physics.OverlapBox(transform.position, new Vector3(5, 5, 4));
        if(cols.Length > 0)
        {
            for(int i = 0; i < cols.Length; i++)
            {
                if(cols[i].tag == "Coin")
                {
                    GoTo go = cols[i].gameObject.AddComponent<GoTo>();
                    go.target = PlayerHandle.instance.transform;
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if(Active)
        {
            
            activeFor -= Time.deltaTime;
            if(activeFor <= 0)
            {
                Active = false;
                

            } 
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Coin" && Active)
        {
            GoTo go = other.gameObject.AddComponent<GoTo>();
            go.target = PlayerHandle.instance.transform;
        }
    }

}
