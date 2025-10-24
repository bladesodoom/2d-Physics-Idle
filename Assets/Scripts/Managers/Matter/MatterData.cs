using System;

using UnityEngine;

[Serializable]
public class MatterData
{
    [Header("Core Stats")]
    public int level = 1;
    public float baseValue = 1f;
    public float damage = 5f;
    public float scale = 1f;

    [Header("Spawn Settings")]
    public int maxActiveMatter = 10;
    public float spawnInterval = 1.5f;

    [Header("Upgrade Levels")]
    public int valueLevel = 0;
    public int damageLevel = 0;
    public int scaleLevel = 0;
    public int maxMatterLevel = 0;
    public int spawnRateLevel = 0;

    public MatterData Clone()
    {
        return (MatterData)this.MemberwiseClone();
    }
}
