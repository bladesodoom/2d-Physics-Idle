using System.Collections.Generic;

using UnityEngine;

public class PegManager : MonoBehaviour
{
    public static PegManager Instance;

    [Header("Peg Settings")]
    public Peg pegPrefab;
    public int rows = 6;
    public int columns = 6;
    public float spacingX = 1.0f;
    public float spacingY = 1.0f;

    [Header("Layout Settings")]
    public Vector2 startOffset = new Vector2(-3.5f, 3.0f);

    [Header("Management")]
    public List<Peg> allPegs = new List<Peg>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GeneratePegGrid();
    }

    public void GeneratePegGrid()
    {
        ClearExistingPegs();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 pos = new Vector3(
                    startOffset.x + col * spacingX + ((row % 2 == 0) ? spacingX / 2f : 0f),
                    startOffset.y - row * spacingY,
                    0f
                );

                Peg newPeg = Instantiate(pegPrefab, pos, Quaternion.identity, transform);
                allPegs.Add(newPeg);
            }
        }
    }

    public void ClearExistingPegs()
    {
        foreach (var peg in allPegs)
        {
            if (peg != null)
            {
                Destroy(peg.gameObject);
            }
        }
        allPegs.Clear();
    }

    public void ResetAllPegs()
    {
        foreach (var peg in allPegs)
        {
            peg.ResetPeg();
        }
    }

    public void UpgradeAllPegs(float efficiencyBoost)
    {
        foreach (var peg in allPegs)
        {
            peg.UpgradeXPMultiplier();
        }
    }
}
