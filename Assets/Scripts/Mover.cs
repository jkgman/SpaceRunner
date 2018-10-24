using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float moveTimeLength;
    private float start;

    private void Start()
    {
        start = Time.time;
        transform.localPosition = startPoint;
    }
    // Update is called once per frame
    void Update () {
        float lerpPoint = Mathf.Clamp01((Time.time - start) / moveTimeLength);
        transform.localPosition = Vector3.Lerp(startPoint, endPoint, lerpPoint);
        if(lerpPoint == 1)
        {
            Destroy(this);
        }
    }
}
