﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {

    public Vector3 eulers;
	void Update () {
        transform.Rotate(eulers);
	}
}
