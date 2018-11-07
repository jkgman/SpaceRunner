using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public Collectable.CollectableType[] _powerUps;

    #region Singleton
    public static ItemManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Itemcontroller found");
            return;
        }
        instance = this;
    }
    #endregion

    public void AddToInv(Collectable collectable)
    {
         
    }


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
