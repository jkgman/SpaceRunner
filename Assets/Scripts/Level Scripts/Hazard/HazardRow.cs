using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds a list of hazards for a row
/// </summary>
public class HazardRow : MonoBehaviour {

    #region Variables
    public Hazard[] hazards;
    public float dist;
    public bool active;
    #endregion

    #region Implementations
    private void Start()
    {
        hazards = GetComponentsInChildren<Hazard>();
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// positions hazards visual
    /// </summary>
    public void Gen() {
        hazards = GetComponentsInChildren<Hazard>();
        for(int i = 0; i < hazards.Length; i++)
        {
            hazards[i].transform.position = transform.position - new Vector3(-4 + 2 * hazards[i].Lane,0,0);
        }
    }
    #endregion
}
