using System;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blackhole : MonoBehaviour
{
    public static Blackhole Instance { get; private set; }

    public event Action OnStatsChanged;

    [Header("Stats")]
    public int collapseCount = 0;

    public float currentMass = 0f;
    public float collapseMass = 500f;
    public float collapseMassScaler = 1.25f;

    [Header("Collapse Rewards")]
    public float singularityPoints = 0f;
    public float spConversionRate = 0.6f;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Absorb(float amount)
    {
        currentMass += amount;
        OnStatsChanged?.Invoke();

        if (currentMass.CompareTo(collapseMass) >= 0)
        {
            Collapse();
            OnStatsChanged?.Invoke();
        }
    }

    private void Collapse()
    {
        collapseCount++;

        singularityPoints += currentMass * spConversionRate;
        currentMass = 0f;
        collapseMass *= collapseMassScaler;
        OnStatsChanged?.Invoke();

        CurrencyManager.Instance.currentCurrency = 0;
        PegManager.Instance.ResetAllPegs();
    }
}
