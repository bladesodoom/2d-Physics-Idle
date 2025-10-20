using System;

using System.Collections.Generic;

[Serializable]
public class GameData
{
    public float currency;

    // Blackhole Data
    public float currentMass;

    // Matter Data
    public int maxMatter;
    public float spawnInterval;
    public float scale;
    public float value;
    public float damage;

    // Peg Data
    public List<PegData> pegDataList = new List<PegData>();

    // General Data
    public double PlayTime;
    public string lastSaveTime;
    public int version = 1;
}
