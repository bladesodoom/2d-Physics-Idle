using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blackhole : MonoBehaviour
{
    public static Blackhole Instance;

    [Header("Stats")]
    private int collapseCount = 0;
    private float currentMass = 0f;
    private float collapseMass = 500f;
    private float collapseMassScaler = 1.25f;

    [Header("Collapse Rewards")]
    private float singularityPoints = 0f;
    private float spConversionrate = 0.2f;

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

        if (currentMass.CompareTo(collapseMass) >= 0)
        {
            Collapse();
        }
    }

    private void Collapse()
    {
        collapseCount++;

        singularityPoints += currentMass * spConversionrate;
        currentMass = 0f;
        collapseMass *= collapseMassScaler;
    }
}
