using System;

[Serializable]
public class SaveData
{
    // Currencies
    public double money;

    // Object Upgrades
    public float objectValue;
    public float objectSize;
    public float objectSpawnRateFactor;
    public int objectMaxQuantity;

    // Obstacle Upgrades
    public float obstacleValue;

    // Dropper Upgrades

    // Multiplier Upgrades

    // Conveyor Upgrades
    public float conveyorSpeed;
    public float conveyorSpawnRate;

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
