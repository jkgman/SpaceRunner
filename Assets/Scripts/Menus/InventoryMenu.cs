using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour /*IDragHandler, IEndDragHandler, IDropHandler, IInitializePotentialDragHandler*/
{
    public Collectable[] _items;
    public int _itemSlotQ;
    private GameObject _selectedObject;
    private Vector2 _ogPosition;

    public GameObject item_f, item_s;
    private GameObject[] _itemslots;
    


    private void Start()
    {
        _items = new Collectable[_itemSlotQ];
        _itemslots = new GameObject[_itemSlotQ];
        _itemslots[0] = item_f;
        _itemslots[1] = item_s;


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

    public void AddItemToSlot(Collectable item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i]==null)
            {
                _items[i] = item;
                _itemslots[i].GetComponent<Image>().sprite = _items[i].UiTexture;
                _itemslots[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            }
        }
        GameManager.Instance.itemSlots = _items;

    }

}

