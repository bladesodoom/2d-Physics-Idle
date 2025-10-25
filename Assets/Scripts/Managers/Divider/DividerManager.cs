using System.Collections.Generic;

using UnityEngine;

public class DividerManager : MonoBehaviour
{
    public static DividerManager Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private GameObject dividerWallPrefab;
    private Transform effectZone;

    [Header("Layout")]
    public int baseDividerCount = 6;
    public float dividerHeight = 0.5f;
    public float wallThickness = 0.1f;
    public float zoneHeight = 1.5f;

    public List<DividerData> allDividers = new();

    private readonly List<GameObject> walls = new();
    private readonly List<GameObject> zones = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        effectZone = dividerWallPrefab.GetComponentInChildren<Transform>();
    }

    private void OnEnable()
    {
        DividerUpgradeManager.OnDividerUpgraded += RebuildDividers;
    }

    private void OnDisable()
    {
        DividerUpgradeManager.OnDividerUpgraded -= RebuildDividers;
    }

    public void BuildDividers()
    {
        ClearDividers();
        allDividers.Clear();

        // Build divider between inner walls above the blackhole
    }

    private void ClearDividers()
    {
        foreach (var w in walls) if (w) Destroy(w);
        foreach (var z in zones) if (z) Destroy(z);
        walls.Clear();
        zones.Clear();
    }

    private int GetCurrentDividerCount() => baseDividerCount + DividerUpgradeManager.Instance.dividerCountBonus;

    private float GetCurrentMultiplier(int index)
    {
        float center = (GetCurrentDividerCount() - 1) / 2f;
        float dist = Mathf.Abs(index - center);
        return Mathf.Lerp(2f, 1f, dist / center) * DividerUpgradeManager.Instance.valueMultiplier;
    }

    public void RebuildDividers() => BuildDividers();
}
