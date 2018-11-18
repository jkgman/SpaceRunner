using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour, IitemEvents
{

    public Collectable[] itemSlots;

    

    #region public variables
    public Button powerUpSlot_f;
    public Button powerUpSlot_s;
    public TextMeshProUGUI text;
    #endregion
    private int coinQ;

    private void Start()
    {
        if (GameManager.Instance.itemSlots!=null) { 
        itemSlots = GameManager.Instance.itemSlots;
        }
        EventSystemListeners.main.AddListener(gameObject);

        if(itemSlots!=null) { 
        powerUpSlot_f.onClick.AddListener(delegate { ConsumeItem(0); });
        powerUpSlot_s.onClick.AddListener(delegate { ConsumeItem(1); });

        powerUpSlot_f.gameObject.GetComponent<Image>().sprite = itemSlots[0].UiTexture;
        powerUpSlot_s.gameObject.GetComponent<Image>().sprite = itemSlots[1].UiTexture;
        }
        text.text = coinQ.ToString();
    }


    void ConsumeItem(int slot)
    {
        if (itemSlots[slot]!=null) { 
        LevelController.instance.SendConsumeMessage(itemSlots[slot]);
        }
    }
    
    public void ItemCollected(Collectable _collectable)
    {
        Debug.Log("itemmanager: message received " + _collectable.type.ToString());

        if (_collectable.type == Collectable.CollectableType.Coin)
        {
            coinQ++;
        }
        text.text = coinQ.ToString();
    }

}
