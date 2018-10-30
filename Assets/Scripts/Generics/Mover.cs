using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generic Mover class
/// Transitions from startPoint to endPoint over moveTimeLength seconds
/// </summary>
public class Mover : MonoBehaviour {

    #region Variables
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float moveTimeLength;
    private float start;
    #endregion

    #region Implements
    private void Start()
    {
        start = Time.time;
        transform.localPosition = startPoint;
    }
    void Update () {
        float lerpPoint = Mathf.Clamp01((Time.time - start) / moveTimeLength);
        transform.localPosition = Vector3.Lerp(startPoint, endPoint, lerpPoint);
        if(lerpPoint == 1)
        {
            Destroy(this);
        }
    }
    #endregion

}
