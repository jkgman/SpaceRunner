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
    public Collectable debugRefresh;
    #endregion

    #region private variables
    public int coinQ { private set; get; }
    
    private Collectable[] lvlItemCopy;
    #endregion

    private void Start()
    {
        EventSystemListeners.main.AddListener(gameObject);
        if (GameManager.Instance != null && GameManager.Instance.itemSlots != null) {
            lvlItems = GameManager.Instance.itemSlots;
            lvlItemCopy = lvlItems;
            AddItems(lvlItems);
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
            //lvlItems[slot] = null;
            buttons[slot].gameObject.SetActive(false);
            //Destroy(buttons[slot].gameObject);
        }
    }


    private void AddItems(Collectable[] levelItems)
    {
        for (int i = 0; i < levelItems.Length; i++)
        {
            if (levelItems[i]!= null )
            {
                //Index for button listener as separate int as delegation returns here when event triggers,
                // thus parameter "i" would point to wrong value
                int index = i;
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick.AddListener(delegate { ConsumeItem( index ); });
                buttons[i].gameObject.GetComponent<Image>().sprite = levelItems[i].UiTexture;
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
        if (_collectable.type == Collectable.CollectableType.Refresh)
        {
            if (lvlItemCopy!=null)
            {
                AddItems(lvlItemCopy);
            }
        }

        //for now
        text.text = coinQ.ToString();
    }


}
