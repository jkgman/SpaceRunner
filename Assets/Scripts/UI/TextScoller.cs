using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScoller : MonoBehaviour {

    TextMeshProUGUI signText;
    public float scrollSpeed;

	// Use this for initialization
	void Start () {
        signText = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + Vector3.left * scrollSpeed * Time.deltaTime;
	}
}
