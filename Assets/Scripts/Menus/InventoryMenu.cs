using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class for inventory handling before actual game scene
/// Remove selected items from saved gama data inventory an onto level item slots
/// 
/// </summary>
public class InventoryMenu : MonoBehaviour /*IDragHandler, IEndDragHandler, IDropHandler, IInitializePotentialDragHandler*/
{
    #region public variables
    public CollectableData[] _items;
    #endregion

    #region private variables
    public Collectable[] itemSlots;
    public int targetSlot = 0;
    private Transform[] itemTransforms;
    #endregion

    private void Start()
    {

        
    }

    private void OnEnable()
    {
        _items = GameManager.Instance.gData.inventoryData;
        itemSlots = new Collectable[2];
    }


    #region Drag code if needed

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (_selectedObject != null) { 
    //        _selectedObject.transform.position = eventData.position;
    //    }
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    if (eventData.hovered.Contains(_itemSlot1))
    //    {
    //        _selectedObject.transform.position = (Vector2)_itemSlot1.transform.position;
    //        _selectedObject = null;
    //    } else if (eventData.hovered.Contains(_itemSlot2))
    //    {
    //        _selectedObject.transform.position = (Vector2)_itemSlot2.transform.position;
    //        _selectedObject = null;
    //    }
    //    else if (_selectedObject != null) {
    //        Transform _buttonPos = _selectedObject.transform;
    //        _buttonPos.transform.position = Vector2.Lerp(_selectedObject.transform.position, _ogPosition, 1f);
    //    }
    //}


    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    _selectedObject = null;
    //}

    //public void OnInitializePotentialDrag(PointerEventData eventData)
    //{
    //    GameObject _pointerObj = eventData.pointerPressRaycast.gameObject;
    //    if ( _pointerObj != _itemSlot1 && _pointerObj != _itemSlot2) { 

    //        _selectedObject = _pointerObj;
    //        _ogPosition = _selectedObject.transform.position;

    //    }
    //}
    #endregion


    /// <summary>
    /// function that moves items from inventory to level item slots and creates and adds listeners to their corresponding buttons
    /// </summary>
    /// <param name="item"></param>
    public void AddItemToSlot(Collectable item)
    {
        if (itemSlots[targetSlot] != null)
        {
            if (item.type == itemSlots[targetSlot].type)
            {
                Debug.Log("Same type item already in item slot!");
                return;
            }
            else
            {
                foreach (var powerup in _items)
                {
                    if (powerup.Collectable.type == itemSlots[targetSlot].type)
                    {
                        Debug.Log(" Switch and return item quantity ");
                        powerup.Quantity++;
                    }
                }
            }
        }


        foreach (var powerup in _items)
        {
            if (powerup.Collectable.type == item.type)
            {
                if (powerup.Quantity > 0)
                {
                    powerup.Quantity--;
                    itemSlots[targetSlot] = powerup.Collectable;

                    //Sprite and alpha change
                    itemTransforms[targetSlot].gameObject.GetComponent<Image>().sprite = itemSlots[targetSlot].UiTexture;
                    itemTransforms[targetSlot].gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    //set the item slot number to next automatically from 1st => 2nd for ease of use
                    if (targetSlot == 0)
                    {
                            targetSlot = 1;
                    }
                }
                else

                {
                    Debug.Log("No Collectables of type! " + powerup.Collectable.type);
                    return;
                }
            }
        }
    }


    #region Item slot functions for UI
    public void SetItemSlot1Transform(Transform target)
    {
        itemTransforms = new Transform[2];
        if (itemTransforms[0] == null)
        {
            itemTransforms[0] = target;
        }
    }
    public void SetItemSlot2Transform(Transform target)
    {
        if (itemTransforms != null)
        {
            if (itemTransforms[1] == null)
            {
                itemTransforms[1] = target;
            }
        }
    }
    public void setTargetSlot1()
    {
        targetSlot = 0;
    }
    public void setTargetSlot2()
    {
        targetSlot = 1;
    }
    #endregion

    private void OnDisable()
    {
        GameManager.Instance.itemSlots = itemSlots;
        GameManager.Instance.gData.inventoryData = _items;
        GameManager.Instance.SaveData(GameManager.Instance.gData);
    }

}

