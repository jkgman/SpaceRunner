using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{

    #region Singleton
    public static ItemManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Itemcontroller found");
            return;
        }
        instance = this;
    }
    #endregion

    public void SendCollectionMessage(Collectable.CollectableType _type)
    {
        foreach (GameObject listener in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IitemEvents>
                (listener, null, (x, y) => x.ItemCollected(_type));
        }
    }
}
