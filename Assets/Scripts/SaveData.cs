using System;
using UnityEditor;

[Serializable]
public class SaveData
{
    // Currencies

    // Falling Object Upgrades

    // Obstacle Upgrades

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
