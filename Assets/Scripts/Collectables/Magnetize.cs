using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetize : MonoBehaviour {
    float activeFor;
    bool Active;
    BoxCollider box;
    public GameObject MagnetPrefab;
    public GameObject magnetvisual;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
    }
    public void Activate(float time) {
        //play mag particle for 10
        magnetvisual.SetActive(true);
        Active = true;
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

                    if (go.gameObject != null)
                    {
                        GameObject mag = Instantiate(MagnetPrefab);
                        mag.GetComponent<MagnetPath>().emitter = gameObject;
                        mag.GetComponent<MagnetPath>().target = go.GetComponentInChildren<Collectable>().gameObject;
                    }
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
                magnetvisual.SetActive(false);

            } 
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Coin" && Active)
        {
            GoTo go = other.gameObject.AddComponent<GoTo>();
            go.target = PlayerHandle.instance.transform;
            if (go.gameObject != null)
            {
                GameObject mag = Instantiate(MagnetPrefab);
                mag.GetComponent<MagnetPath>().emitter = gameObject;
                mag.GetComponent<MagnetPath>().target = go.gameObject;
            }
        }
    }

}
