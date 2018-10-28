using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Displays Rotater distance to a textmesh pro text object
/// </summary>
public class Distance : MonoBehaviour {

    #region Variables
    private LevelController controller;
    private TextMeshProUGUI text;
    #endregion

    #region Implementations
    /// <summary>
    /// Assign references
    /// </summary>
    void Start () {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        controller = LevelController.instance;
        if(!text)
        {
            Debug.LogError("Distance Missing TextMeshProUGUI reference");
        }
        if(!controller)
        {
            Debug.LogError("Distance Missing Rotater reference");
        }
    }
	
	/// <summary>
    /// Update distance ran in ui
    /// </summary>
	void Update () {
        text.text = Mathf.RoundToInt(controller.distance) + " m";
    }
    #endregion

}
