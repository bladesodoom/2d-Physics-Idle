using UnityEngine;

public class DropperManager : UpgradeManager<DropperManager>
{
    public float spawnRate { get; private set; }
    protected override void InitializeStats()
    {
        spawnRate = base.SaveData.spawnRate;
    }

    public void UpgradeSpawnRate()
    {
        spawnRate *= 1.1f;
        WriteToSave();
    }

    protected override void WriteToSave()
    {
        base.SaveData.spawnRate = spawnRate;
    }
}
