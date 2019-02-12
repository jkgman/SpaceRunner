using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Interface for defining item collection and destruction messages 
/// </summary>
public interface IitemEvents : IEventSystemHandler  {

    void ItemCollected( Collectable _collectable);
    
}
