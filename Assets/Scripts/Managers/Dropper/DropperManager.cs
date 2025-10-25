using System.Collections.Generic;

using UnityEngine;

public class DropperManager : MonoBehaviour
{
    public static DropperManager Instance { get; private set; }

    [Header("Dropper Stats")]
    public int minMatterTier = 0;
    public int maxMatterTier = 2;
    public float spawnRate = 1.0f;
    public int spawnCount = 1;
    public float additionalMatterValue = 1.0f;
    public float dropperWidth = 5.0f;

    [Header("Droppers")]
    public List<Dropper> allDroppers = new();
    public Dropper dropperPrefab;

    private void OnEnable()
    {
        DropperUpgradeManager.OnDropperUpgraded += ApplyUpgrades;
    }

    private void OnDisable()
    {
        DropperUpgradeManager.OnDropperUpgraded -= ApplyUpgrades;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void ApplyUpgrades()
    {
        // Reapply values or rebuild any dependent systems (e.g. Dropper positions)
        Debug.Log("[DropperManager] Dropper stats updated!");
    }

    public void ApplyTierData(DropperData data)
    {
        foreach (var dropper in allDroppers)
        {
            dropper.ApplyTierStats(data);
        }
    }
}
