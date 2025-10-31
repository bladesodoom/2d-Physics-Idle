using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Matter : MonoBehaviour
{
    [Header("Stats")]
    public float value { get; private set; } = 1;
    public float size { get; private set; } = 1;
    public float damage { get; private set; } = 8;


    public void IncreaseValue(float amount)
    {
        value += amount;
    }

    public void DecreaseSize()
    {
        size *= 0.95f;
    }

    public void DecreaseDamage()
    {
        damage *= 0.95f;
    }
}