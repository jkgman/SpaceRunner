using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Class for inventory, energy, level and rating tracking.
/// Saving gamedata as json 
/// 
/// </summary>
public class GameManager : MonoBehaviour {

    #region private variables
    private string gameDataFileName = "gamedata";
    #endregion

    #region public variables
    public Collectable[] itemSlots;
    public GameData gData;
    #endregion

    #region singleton
    public static GameManager Instance = null;

    // Initialize singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    #endregion

	
	// Update is called once per frame
    //Debug testing the saving and loadign
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SaveData(gData);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadData();
            
        }
    }


    /// <summary>
    /// Loads data from json file. returns Gamedata which includes progression
    /// Returns error if no file is found
    /// </summary>
    /// <returns></returns>
    public GameData LoadData()
    {
        GameData loadedData;
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName + ".json").Replace("\\", "/");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot find gamedata file!");
            return null;
        }
        return loadedData;
    }


    /// <summary>
    /// save the parameter sent game data as json file.
    /// No encryption for now
    /// </summary>
    /// <param name="gdata"></param>
    public void SaveData(GameData gdata)
    {
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName + ".json");
        string jsonData = JsonUtility.ToJson(gdata);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Saved!");
    }

}

/// <summary>
/// Gamedata class. Here for now
/// </summary>
[System.Serializable]
public class GameData
{
    public int progression;
    public int energy;
    public int inventorySize;
    public Collectable[] inventoryData;
}