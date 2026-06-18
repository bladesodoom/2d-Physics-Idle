using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Ball : MonoBehaviour
{
    [Header("Stats")]
    public float value { get; private set; }
    public float size { get; private set; }
    public int maxQuantity {get; private set; }

    public void InitializeStats(float saveValue, float saveSize, int saveMaxQuantity)
    {
        value = saveValue; size = saveSize; maxQuantity = saveMaxQuantity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
            float amount = obstacle.Value;
            IncreaseValue(amount);
        }
    }

    public void IncreaseValue(float amount)
    {
        value += amount;
    }

    public void DecreaseValue(float amount)
    {
        value = Mathf.Max(0, value - amount);
    }
}