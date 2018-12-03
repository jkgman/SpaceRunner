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
    public Button powerUpSlot_f;
    public Button powerUpSlot_s;
    public TextMeshProUGUI text;
    public Collectable[] itemSlots;
    #endregion
    private int coinQ;
    private GameObject[] items;

    //Spaghetti
    private void Start()
    {
        EventSystemListeners.main.AddListener(gameObject);
        if (GameManager.Instance!= null)
        {
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
         if (itemSlots[slot]!=null) { 
            LevelController.instance.SendConsumeMessage(itemSlots[slot]);
            itemSlots[slot] = null;
            Destroy(items[slot]);

        }
    }
    

    private void AddItems()
    {
        items = new GameObject[2];
        if (GameManager.Instance.itemSlots != null)
        {
            itemSlots = GameManager.Instance.itemSlots;
        }
        EventSystemListeners.main.AddListener(gameObject);

        if (itemSlots != null)
        {
            powerUpSlot_f.onClick.AddListener(delegate { ConsumeItem(0); });
            powerUpSlot_s.onClick.AddListener(delegate { ConsumeItem(1); });

            items[0] = powerUpSlot_f.gameObject;
            items[1] = powerUpSlot_s.gameObject;

            powerUpSlot_f.gameObject.GetComponent<Image>().sprite = itemSlots[0].UiTexture;
            powerUpSlot_s.gameObject.GetComponent<Image>().sprite = itemSlots[1].UiTexture;
            powerUpSlot_f.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            powerUpSlot_s.GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
        if(_collectable.type == Collectable.CollectableType.Magnet)
        {
            PlayerHandle.instance.Magnet(10f);
        }
        //for now
        text.text = coinQ.ToString();
    }

}
