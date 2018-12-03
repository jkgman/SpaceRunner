using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Security.Cryptography;

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

    private void Start()
    {
        itemSlots = null;
        if (LoadData() != null) { 
        gData = LoadData();
        } else
        {
            GameData newData = new GameData();
            newData.energy = 100;
            newData.inventorySize = 5;
            newData.levelProgression = new int[0];
            newData.inventorySize = 3;

            InitNewData( newData );
            gData = newData;
        }
    }

    /// <summary>
    /// Initialize new save data
    /// </summary>
    /// <param name="newData"></param>
    private void InitNewData(GameData newData)
    {
        SaveData(newData);
    }


    private void Update()
    {
        //for save testing
        if (Input.GetKeyDown(KeyCode.A))
        {
            SaveData(gData);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gData = LoadData();
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

            //byte[] dataAsJson = File.ReadAllBytes(filePath);
            //string stringData = Decrypt(dataAsJson);
            //loadedData = JsonUtility.FromJson<GameData>(stringData);

            byte[] soupBackIn = File.ReadAllBytes(filePath);
            string jsonFromFile = crypto.Decrypt(soupBackIn, hash);

            GameData copy = JsonUtility.FromJson<GameData>(jsonFromFile);
            Debug.Log(copy.energy);
            loadedData = copy;
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



}

