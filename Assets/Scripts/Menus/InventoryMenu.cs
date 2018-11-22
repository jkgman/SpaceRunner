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
    public Collectable[] _items;
    public int _itemSlotQ;
    public GameObject item_f, item_s;
    #endregion

    #region private variables
    private GameObject _selectedObject;
    private Vector2 _ogPosition;
    private Collectable[] itemSlots;
    #endregion

    private void Start()
    {
        _items = GameManager.Instance.gData.inventoryData;
        PopulateInventoryUI(_items);
        itemSlots = new Collectable[2];
    }
    

    // Remove level items from saved game inventory
    // check that moved to level and not back to menu
    private void OnDisable()
    {
        GameManager.Instance.itemSlots = itemSlots;
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
    /// Placeholder function for handling moving items from inventory to level item slots
    /// </summary>
    /// <param name="item"></param>
    public void AddItemToSlot(Collectable item)
    {
        if (itemSlots[0]== null)
        {
            itemSlots[0] = item;
            item_f.GetComponent<Image>().sprite = item.UiTexture;
            item_f.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        } else if (itemSlots[1] == null)
        {
            itemSlots[1] = item;
            item_s.GetComponent<Image>().sprite = item.UiTexture;
            item_s.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

    }


    private void PopulateInventoryUI(Collectable[] items)
    {




    }

}

