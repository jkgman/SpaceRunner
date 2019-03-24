using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUi : MonoBehaviour, IitemEvents {


    private TextMeshProUGUI text;
    private Collectable[] levelInventory;

    public void ItemCollected(Collectable.CollectableType _collectable)
    {
        text.text = _collectable.ToString();
        levelInventory = new Collectable[2];
    }


    // Use this for initialization
    void Start () {
        EventSystemListeners.main.AddListener(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
