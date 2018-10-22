using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _playerPrefab;

    private GameObject _activePlayer;
	// Use this for initialization
	void Start () {
        if (_activePlayer == null)
        {
            _activePlayer = Instantiate(_playerPrefab);
        }
    }



    public void SpawnPlayer()
    {
        if (_activePlayer == null) {
        _activePlayer = Instantiate(_playerPrefab); 
        }
    }
}
