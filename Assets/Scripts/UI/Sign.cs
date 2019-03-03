using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {

    public GameObject prefabText;
    private GameObject signText;
    private GameObject textDouble;
	// Use this for initialization
	void Start () {
        signText = GameObject.Instantiate(prefabText,transform);
	}
	
	// Update is called once per frame
	void Update () {

        if (textDouble != null && textDouble.transform.position.x < Screen.width / 4 )
        {
            if (signText.transform.position.x < 0) { 
            signText.transform.position = new Vector3( Screen.width*1.25f, transform.position.y,transform.position.z);
            }
        }

        if ( signText.transform.position.x < Screen.width / 4)
        {
            if (textDouble == null)
            {
                textDouble = Instantiate(prefabText, new Vector3(Screen.width * 1.25f, transform.position.y, transform.position.z)
                , new Quaternion(0, 0, 0, 0), transform);
            }
            else
            {
                if (textDouble.transform.position.x < 0)
                {
                    textDouble.transform.position = new Vector3(Screen.width * 1.25f, transform.position.y, transform.position.z);
                }
            }
        }

    }
}
