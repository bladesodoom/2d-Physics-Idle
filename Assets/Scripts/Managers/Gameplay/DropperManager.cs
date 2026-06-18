using UnityEngine;

public class DropperManager : UpgradeManager<DropperManager>
{
    public float spawnRate { get; private set; }
    public void InitializeStats(SaveData saveData)
    {
        spawnRate = saveData.spawnRate;
    }

    public void UpgradeSpawnRate()
    {
        spawnRate *= 1.1f;
        WriteToSave();
    }
}
