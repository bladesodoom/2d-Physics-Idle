using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FObject : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float value;
    [SerializeField] private float spawnRateFactor;

    public float Value { get => value; }

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
}