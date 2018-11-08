using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IitemEvents : IEventSystemHandler  {

    void ItemCollected( Collectable.CollectableType type);
    
}
