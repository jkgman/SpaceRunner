using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Displays Rotater distance to a textmesh pro text object
/// </summary>
public class Distance : MonoBehaviour {

    #region Variables
    private Rotate rotater;
    private TextMeshProUGUI text;
    #endregion

    #region Implementations
    /// <summary>
    /// Assign references
    /// </summary>
    void Start () {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        rotater = FindObjectOfType<Rotate>();
        if(!text)
        {
            Debug.LogError("Distance Missing TextMeshProUGUI reference");
        }
        if(!rotater)
        {
            Debug.LogError("Distance Missing Rotater reference");
        }
    }
	
	/// <summary>
    /// Update distance ran in ui
    /// </summary>
	void Update () {
        text.text = Mathf.RoundToInt(rotater.Distance) + " m";
    }
    #endregion

}
