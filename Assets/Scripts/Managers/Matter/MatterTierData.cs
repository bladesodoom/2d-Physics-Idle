using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class MatterTierData
{
    public int currentTier = 0;
    public List<MatterData> tierList = new();

    public MatterData CurrentTierData => tierList[Mathf.Clamp(currentTier, 0, tierList.Count - 1)];

    public bool TryAdvanceTier()
    {
        if (currentTier + 1 < tierList.Count)
        {
            currentTier++;
            return true;
        }
        return false;
    }
}
