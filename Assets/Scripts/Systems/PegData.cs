using System;

using UnityEngine;

[Serializable]
public class PegData
{
    public int id;
    public Vector3 pegPosition;
    public int level;
    public float upgradeCost;
    public float value;
    public float currentXP;
    public float xpNextLevel;
    public float xpGainMultiplier;
    public float levelScaler;
    public float currentXPValue;
    public float productionBoost;
}
