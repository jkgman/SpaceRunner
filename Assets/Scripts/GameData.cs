using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Gamedata class
/// </summary>
public class GameData {
    public int energy;
    public int inventorySize;
    public int[] levelProgression;
    public Collectable[] inventoryData;
}
