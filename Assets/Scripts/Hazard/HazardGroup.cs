using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds a list of hazards for sets
/// </summary>
public class HazardGroup : MonoBehaviour {
    public Hazard[] hazards;
    public float dist;
    public bool active;
    //TODO: (Possibility) make this a class that holds pre aranged sets of hazards, if we want to abandon complete random
    private void Start()
    {
        hazards = GetComponentsInChildren<Hazard>();
    }

    //has length
    //hold list of hazards in group
    public void Gen() {
        hazards = GetComponentsInChildren<Hazard>();
        for(int i = 0; i < hazards.Length; i++)
        {
            hazards[i].transform.position = transform.position - new Vector3(-4 + 2 * hazards[i].Lane,0,0);
        }
    }

}
