﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UITest : MonoBehaviour, IitemEvents {
    private TextMeshProUGUI text;

    public void ItemCollected(Collectable.CollectableType _collectable)
    {
        text.text = _collectable.ToString();
    }


    // Use this for initialization
    void Start () {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        EventSystemListeners.main.AddListener(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
    
}
