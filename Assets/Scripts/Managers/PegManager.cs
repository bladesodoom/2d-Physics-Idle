using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PegManager : MonoBehaviour
{
    public static PegManager Instance { get; private set; }

    [Header("Peg Settings")]
    public Peg pegPrefab;
    public GameObject pegUpgradeMenu;

    private Peg selectedPeg;

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
        DontDestroyOnLoad(gameObject);
    }

    public void SelectPeg(Peg peg)
    {
        selectedPeg = peg;
        PegUpgradeManager.Instance.OpenMenu(peg);
    }

    public List<PegData> GetAllPegData()
    {
        List<PegData> dataList = new List<PegData>();

        foreach (Peg peg in allPegs)
        {
            if (peg != null) dataList.Add(peg.ToData());
        }
        return dataList;
    }

    public void ScheduleRespawn(Peg peg, float delay)
    {
        StartCoroutine(RespawnPegAfterDelay(peg, delay));
    }

    private IEnumerator RespawnPegAfterDelay(Peg peg, float delay)
    {
        yield return new WaitForSeconds(delay);
        peg.gameObject.SetActive(true);
        peg.ResetPeg();
        peg.sr.enabled = true;
        peg.sr.color = peg.defaultColor;
        peg.isRespawning = false;
    }

    private void Start()
    {
        GeneratePegGrid();
    }

    public void GeneratePegGrid(List<PegData> loadedPegData = null)
    {
        ClearExistingPegs();

        int total = rows * columns;
        int dataCount = loadedPegData != null ? loadedPegData.Count : 0;
        int index = 0;
        int pegIDCounter = 0;

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

                newPeg.pegID = pegIDCounter++;
                newPeg.gameObject.name = $"Peg_{pegIDCounter}";
                if (index < dataCount)
                {
                    newPeg.FromData(loadedPegData[index]);
                }
                allPegs.Add(newPeg);
                index++;
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
}
