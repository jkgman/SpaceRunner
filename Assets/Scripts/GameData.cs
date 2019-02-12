using UnityEngine;

[System.Serializable]
/// <summary>
/// Gamedata class
/// </summary>
public class GameData {
    public int energy;
    public int[] levelProgression;
    public int[] levelScores;
    public CollectableData[] inventoryData;
    public bool runTutoPrompt;

}


