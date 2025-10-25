using System.Collections.Generic;

using UnityEngine;

public class PegManager : MonoBehaviour
{
    public static PegManager Instance { get; private set; }

    [Header("Peg Setup")]
    public Peg pegPrefab;
    public List<Peg> allPegs = new();

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

    public void ApplyTierData(PegData data)
    {
        foreach (var peg in allPegs)
        {
            peg.ApplyTierStats(data);
        }
    }
}
