using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Security.Cryptography;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Class for inventory, energy, level and rating tracking.
/// Saving gamedata as json 
/// 
/// </summary>
public class GameManager : MonoBehaviour {

    #region private variables
    private string gameDataFileName = "spacedata";
    #endregion

    #region public variables
    [HideInInspector]
    public Collectable[] itemSlots;
    //[HideInInspector]
    public GameData gData;
    public int LevelQuantity;
    public int currentLevel;
    //public GameData DebugData;
    public GameData newGameData;
    public TextMeshProUGUI text;
    #endregion

    #region singleton
    public static GameManager Instance = null;

    // Initialize singleton instance and load data
    private void Awake()
    {
        // If there is not already an instance of Gamemanager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set Gamemanager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);

        
    }
    #endregion

    private void Start()
    {
        gData = null;
        itemSlots = null;

        
        gData = LoadData();

        if (gData == null)
        {
            text.text = "New data!";
            Debug.Log("Initialized new data");
            gData = newGameData;
            gData.firstRun = false;
            SaveData(gData);
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
            Rijndael crypto = new Rijndael();

            byte[] soupBackIn = File.ReadAllBytes(filePath);
            string jsonFromFile = crypto.Decrypt(soupBackIn, hash);

            GameData copy = JsonUtility.FromJson<GameData>(jsonFromFile);
            loadedData = copy;
            text.text = "LOADED!";
            if (loadedData.firstRun == true)
            {
                return null;
            }
        }
        else
        {
            //Debug.LogError("Cannot find gamedata file!");
            text.text = "No file!";
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

        Rijndael crypto = new Rijndael();
        byte[] soup = crypto.Encrypt(jsonData, hash);

        //string filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        File.WriteAllBytes(filePath, soup);
        Debug.Log("Saved!");
    }

    //Encryption key
    private static string hash = "sp4c3runn3r!sp4c3runn3r!";

#if UNITY_EDITOR
    [CustomEditor(typeof(GameManager))]
    public class GameDataEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GameManager script = (GameManager)target;
            if (GUILayout.Button("Save Data"))
            {
                script.SaveData(script.newGameData);
            }
        }
    }
#endif

    //Save for those nasty quitters
    private void OnApplicationQuit()
    {
        SaveData(gData);
    }

}

