using System.Collections.Generic;

using UnityEngine;

public class DividerManager : MonoBehaviour
{
    public static DividerManager Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private GameObject dividerWallPrefab;
    [SerializeField] private GameObject dividerZonePrefab;

    [Header("References")]
    [SerializeField] private BoardManager boardManager;

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

        Rect board = boardManager.BoardBounds;
        float yPos = board.yMin + dividerHeight * 0.5f;
        int count = GetCurrentDividerCount();
        float spacing = board.width / count;
        float startX = board.xMin;

        for (int i = 0; i <= count; i++)
        {
            float wallX = startX + i * spacing;
            Vector2 wallPos = new(wallX, yPos);
            GameObject wall = Instantiate(dividerWallPrefab, wallPos, Quaternion.identity, transform);
            wall.transform.localScale = new Vector3(wallThickness, dividerHeight, 1f);
            wall.name = $"DividerWall_{i}";
            walls.Add(wall);

            if (i > 0)
            {
                float zoneX = startX + (i - 0.5f) * spacing;
                Vector2 zonePos = new(zoneX, yPos + zoneHeight * 0.5f);

                GameObject zone = Instantiate(dividerZonePrefab, zonePos, Quaternion.identity, transform);
                zone.transform.localScale = new Vector3(spacing - wallThickness, zoneHeight, 1f);
                zone.name = $"DividerZone_{i - 1}";

                var trigger = zone.GetComponent<DividerZoneTrigger>();
                if (trigger == null)
                    trigger = zone.AddComponent<DividerZoneTrigger>();

                DividerData data = new DividerData
                {
                    index = i - 1,
                    multiplier = GetCurrentMultiplier(i - 1),
                    width = spacing,
                    position = zonePos
                };

                trigger.Setup(data);
                allDividers.Add(data);
                zones.Add(zone);
            }
        }

        Debug.Log($"[DividerManager] Built {zones.Count} zones and {walls.Count} walls.");
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
