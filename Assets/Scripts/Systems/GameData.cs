using System;

using System.Collections.Generic;

[Serializable]
public class GameData
{
    public float currency;

    // Blackhole Data
    public float currentMass;
    public float singularityPoints;
    public int collapseCount;
    public float collapseMass;
    public float collapseMassScaler;
    public float spConversionRate;

    // Matter Data
    public int maxMatter;
    public float spawnInterval;
    public float xScale;
    public float yScale;
    public float value;
    public float damage;

    // Peg Data
    public List<PegData> pegDataList = new List<PegData>();

    // General Data
    public double PlayTime;
    public string lastSaveTime;
    public int version = 1;
}
