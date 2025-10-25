using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Builds and rebuilds the peg grid dynamically based on board bounds,
/// peg size, matter size, and quantity upgrades.
/// </summary>
public class PegBuilder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Peg pegPrefab;
    [SerializeField] private PegManager pegManager;
    [SerializeField] private MatterManager matterManager;
    [SerializeField] private BoardManager boardManager;

    public float spacingMultiplier = 1.2f;

    private readonly List<Peg> pegs = new();

    public void BuildGrid()
    {
        ClearPegs();

        float matterSize = matterManager.CurrentMatterSize;
        float pegSize = GetCurrentPegSize();
        int quantityLevel = GetCurrentPegQuantityLevel();
        Rect boardBounds = boardManager.BoardBounds;

        float distance = pegSize + (matterSize * spacingMultiplier);
        float verticalSpacing = distance * 0.866f;

        float densityFactor = 1f + (quantityLevel * 0.1f);
        distance /= densityFactor;
        verticalSpacing /= densityFactor;

        int cols = Mathf.FloorToInt(boardBounds.width / distance);
        int rows = Mathf.FloorToInt(boardBounds.height / verticalSpacing);

        float totalWidth = cols * distance;
        float totalHeight = rows * verticalSpacing;
        float startX = boardBounds.center.x - (totalWidth / 2f) + (distance / 2f);
        float startY = boardBounds.center.y - (totalHeight / 2f) + (verticalSpacing / 2f);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                float offsetX = (y % 2 == 1) ? distance / 2f : 0f;
                float posX = startX + x * distance + offsetX;
                float posY = startY + y * verticalSpacing;

                if (posX < boardBounds.xMin + pegSize / 2f || posX > boardBounds.xMax - pegSize / 2f ||
                    posY < boardBounds.yMin + pegSize / 2f || posY > boardBounds.yMax - pegSize / 2f)
                    continue;

                Vector2 position = new(posX, posY);
                Peg peg = Instantiate(pegPrefab, position, Quaternion.identity, transform);
                peg.transform.localScale = Vector3.one * pegSize;
                peg.name = $"Peg_{x}_{y}";

                pegs.Add(peg);
                pegManager.allPegs.Add(peg);
            }
        }

        Debug.Log($"[PegBuilder] Built {pegs.Count} pegs ({cols}x{rows}) at density {densityFactor:F2}");
    }
    public void ClearPegs()
    {
        foreach (Peg peg in pegs)
        {
            if (peg != null)
                Destroy(peg.gameObject);
        }

        pegs.Clear();
        pegManager.allPegs.Clear();
    }

    public void RebuildGrid() => BuildGrid();

    private float GetCurrentPegSize()
    {
        // Option 1: Use PegData.size when implemented
        PegData currentTier = PegTierManager.Instance.tierData.CurrentTierData;
        if (currentTier.size > 0)
            return currentTier.size;

        // Option 2: Default fallback value (temporary)
        return 0.5f;
    }

    private int GetCurrentPegQuantityLevel()
    {
        return PegUpgradeManager.Instance != null ? PegUpgradeManager.Instance.valueLevel : 0;
    }
}
