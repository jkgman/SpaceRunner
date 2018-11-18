using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour {

    #region private variables
    private string gameDataFileName = "gamedata";
    #endregion
    public Collectable[] itemSlots;
    public TextMeshProUGUI text;
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

    // Use this for initialization
    void Start () {
           
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SaveData(new GameData());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadData();
            
        }
    }



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

    public void SaveData(GameData gdata)
    {
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName + ".json");
        string jsonData = JsonUtility.ToJson(gdata);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Saved!");
    }

}

[System.Serializable]
public class GameData
{
    public int progression;
    public int energy;
    public List<Collectable> inventoryData = new List<Collectable>();
}