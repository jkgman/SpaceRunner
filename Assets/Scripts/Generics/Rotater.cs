using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {

    public Vector3 eulers;
    public Space space = Space.Self;
	void Update () {
        transform.Rotate(eulers.x, eulers.y, eulers.z, space);
	}
}
