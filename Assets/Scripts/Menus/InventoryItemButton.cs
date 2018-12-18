﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Gets item quantity from loaded data and changes ui accordingly.
/// Generalized
/// Drag script to new item button with text child and select type in inspector to use on new object.
/// </summary>
public class InventoryItemButton : MonoBehaviour {

    private Button button;
    private int itemQuantity;

    public Collectable.CollectableType refbagItemtype;
    private TMP_Text qText;
    private CollectableData bagItem;

    private void Start()
    {
        
        button = GetComponent<Button>();
        qText = GetComponentInChildren<TMP_Text>();
        //GameManager.Instance.gData = GameManager.Instance.LoadData();

        CollectableData[] inventoryData = GameManager.Instance.gData.inventoryData;
            
        for (int i = 0; i < GameManager.Instance.gData.inventoryData.Length; i++)
        {
            if (inventoryData[i].Collectable.type == refbagItemtype)
            {
                bagItem = inventoryData[i];
                itemQuantity = bagItem.Quantity;
                qText.text = "x" + itemQuantity.ToString();
            }
        }
        //button.onClick.AddListener(InventoryItem);
    }


    //void InventoryItem()
    //{
    //    itemQuantity = bagItem.Quantity;
    //    qText.text = "x" + itemQuantity.ToString();
    //}

    private void Update()
    {
        itemQuantity = bagItem.Quantity;
        qText.text = "x" + itemQuantity.ToString();
    }
}