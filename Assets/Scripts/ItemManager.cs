using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour, IitemEvents
{
    private void Start()
    {
        EventSystemListeners.main.AddListener(gameObject);
    }

    
    public void ItemCollected(Collectable.CollectableType type)
    {
        Debug.Log("itemmanager: message received");
        
    }


}
