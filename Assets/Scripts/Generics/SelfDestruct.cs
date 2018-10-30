using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Destroy object after DestructInSeconds 
/// </summary>
public class SelfDestruct : MonoBehaviour {

    #region Variables
    private float time;
    [SerializeField]
    private float destructInSec;
    #endregion

    #region Implementations
    void Update () {
        time += Time.deltaTime;
        if(time>=destructInSec)
        {
            Destroy(gameObject);
        }
	}
    #endregion
}

