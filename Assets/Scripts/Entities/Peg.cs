using UnityEngine;

public class Peg : MonoBehaviour
{
    public float currentHP;
    public float pegValue;
    public float xpNextLevel;
    public float xpGainMultiplier;
    public float upgradeCost;

    public void ApplyTierStats(PegData data)
    {
        pegValue = data.baseValue;
        currentHP = data.maxHP;
        upgradeCost = data.upgradeCost;
    }
}
