using System;

using UnityEngine;

public class PegUpgradeManager : MonoBehaviour
{
    public static PegUpgradeManager Instance { get; private set; }


    public event Action OnStatsChanged;

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

    private void Start()
    {
        Debug.LogWarning("Peg Upgrade Manager has not been implemented yet.");
    }

}
