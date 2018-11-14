using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMenu : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IDropHandler, IInitializePotentialDragHandler
{
    private GameObject _selectedObject;
    private Vector2 _ogPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerEnter);
        //Debug.Log(eventData.pointerPressRaycast.gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_selectedObject != null) { 
        _selectedObject.transform.position = eventData.position;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        if (_selectedObject != null) { 
        _selectedObject.transform.position = Vector2.Lerp(_selectedObject.transform.position, _ogPosition, 1f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _selectedObject = null;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerEnter);
        //Debug.Log(eventData.pointerPress.gameObject);

        if (eventData.pointerPressRaycast.gameObject != gameObject) { 
            
            _selectedObject = eventData.pointerPressRaycast.gameObject;
            _ogPosition = _selectedObject.transform.position;
        }
    }

    // Use this for initialization
}
