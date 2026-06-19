using System;

[Serializable]
public class SaveData
{
    // Currencies
    public double money;

    // Ball Upgrades
    public float ballValue;
    public float ballSize;
    public int ballMaxQuantity;

    // Obstacle Upgrades
    public float obstacleValue;

    // Dropper Upgrades
    public float spawnRate;

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
            ballValue = 0f,
            ballSize = 2,
            ballMaxQuantity = 20,
            obstacleValue = 0f,
            spawnRate = 2f,
            conveyorSpeed = 1f,
            conveyorSpawnRate = 2f,
            lastSaveTime = DateTime.Now.ToString()
        };
    }
}
