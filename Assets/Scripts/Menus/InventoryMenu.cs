using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMenu : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler, IInitializePotentialDragHandler
{
    private GameObject _selectedObject;
    private Vector2 _ogPosition;

    public GameObject _itemSlot1;
    public GameObject _itemSlot2;

    public void OnDrag(PointerEventData eventData)
    {
        if (_selectedObject != null) { 
            _selectedObject.transform.position = eventData.position;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.hovered.Contains(_itemSlot1))
        {
            _selectedObject.transform.position = (Vector2)_itemSlot1.transform.position;
            _selectedObject = null;
        } else if (eventData.hovered.Contains(_itemSlot2))
        {
            _selectedObject.transform.position = (Vector2)_itemSlot2.transform.position;
            _selectedObject = null;
        }
        else if (_selectedObject != null) {
            Transform _buttonPos = _selectedObject.transform;
            _buttonPos.transform.position = Vector2.Lerp(_selectedObject.transform.position, _ogPosition, 1f);
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _selectedObject = null;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        GameObject _pointerObj = eventData.pointerPressRaycast.gameObject;
        if ( _pointerObj != _itemSlot1 && _pointerObj != _itemSlot2) { 
            
            _selectedObject = _pointerObj;
            _ogPosition = _selectedObject.transform.position;
        }
    }
}
