using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// In level stored item handling/consumption
/// </summary>
public class ItemManager : MonoBehaviour, IitemEvents
{
    #region public variables
    public Button[] buttons;
    public TextMeshProUGUI text;
    public Collectable[] lvlItems;
    #endregion
    private int coinQ;


    private void Start()
    {
        EventSystemListeners.main.AddListener(gameObject);
        if (GameManager.Instance != null && GameManager.Instance.itemSlots != null) {
            lvlItems = GameManager.Instance.itemSlots;
            AddItems();
        }

        text.text = "0";
    }

    /// <summary>
    /// Use item in the level inventory slot
    /// </summary>
    /// <param name="slot"></param>
    void ConsumeItem(int slot)
    {
         if ( lvlItems[slot] !=null) { 
            LevelController.instance.SendConsumeMessage(lvlItems[slot]);
            lvlItems[slot] = null;
            Destroy(buttons[slot].gameObject);
        }
    }


    private void AddItems()
    {
        for (int i = 0; i < lvlItems.Length; i++)
        {
            if (lvlItems[i]!= null )
            {
                //Index for button listener as separate int as delegation returns here when event triggers,
                // thus parameter "i" would point to wrong value
                int index = i;
                buttons[i].onClick.AddListener(delegate { ConsumeItem( index ); });
                buttons[i].gameObject.GetComponent<Image>().sprite = lvlItems[i].UiTexture;
                buttons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }

        text.text = coinQ.ToString();
    }

    /// <summary>
    /// Item was collected
    /// </summary>
    /// <param name="_collectable"></param>
    public void ItemCollected(Collectable _collectable)
    {
        Debug.Log("itemmanager: message received " + _collectable.type.ToString());

        if (_collectable.type == Collectable.CollectableType.Coin)
        {
            coinQ++;
        }
        //for now
        text.text = coinQ.ToString();
    }

}
