using System;
using UnityEditor;

[Serializable]
public class SaveData
{
    // Currencies
    public float money;

    // Object Upgrades
    public float objectValue;
    public float size;
    public float spawnRateFactor;
    public int maxQuantity;

    // Obstacle Upgrades
    public float obstacleValue;

    // Dropper Upgrades

    // Multiplier Upgrades

    // Meta
    public string lastSaveTime;

    public static SaveData NewGame()
    {
        return new SaveData
        {
            // Initialize default values for a new game here
        };
    }
}
