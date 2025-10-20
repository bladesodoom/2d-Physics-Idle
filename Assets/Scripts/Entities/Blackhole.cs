using System;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blackhole : MonoBehaviour
{
    public static Blackhole Instance { get; private set; }

    public event Action OnStatsChanged;

    [Header("Stats")]
    public float currentMass = 0f;

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
    }
}
