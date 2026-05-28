using System;
using UnityEditor;

[Serializable]
public class SaveData
{
    // Currencies
    public float money = 0f;

    // Object Upgrades
    public float objectValue = 1f;
    public float objectSize = 2f;
    public float objectSpawnRateFactor = 0.2f;
    public int objectMaxQuantity = 20;

    // Obstacle Upgrades
    public float obstacleValue = 0f;

    // Dropper Upgrades

    // Multiplier Upgrades

    // Conveyor Upgrades
    public float conveyorSpeed = 1f;
    public float conveyorSpawnRate = 2f;

    // Meta
    public string lastSaveTime;

    public static SaveData NewGame()
    {
        return new SaveData
        {
            // Initialize default values for a new game here
            money = 0f,
            objectValue = 0f,
            objectSize = 2,
            objectSpawnRateFactor = 0.2f,
            objectMaxQuantity = 20,
            obstacleValue = 0f,
            conveyorSpeed = 1f,
            conveyorSpawnRate = 2f,
            lastSaveTime = DateTime.Now.ToString()
        };
    }
}
