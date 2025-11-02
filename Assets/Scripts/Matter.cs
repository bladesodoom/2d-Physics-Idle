using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Matter : MonoBehaviour
{
    [Header("Stats")]
    [Range(0.5f, 5)][SerializeField] private float scale = 3;
    [SerializeField] private float value = 1;
    [SerializeField] private float damage = 5;

    public float Scale { get => scale; }
    public float Value { get => value; }
    public float Damage { get => damage; }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Peg"))
        {
            Peg peg = other.gameObject.GetComponent<Peg>();
            float amount = peg.Value;
            IncreaseValue(amount);
            // Spawn floating text with currency color, positive Y, at collision point
        }
    }
    public void IncreaseValue(float amount)
    {
        value += amount;
    }

    public void DecreaseSize()
    {
        scale *= 0.95f;
    }

    public void DecreaseDamage()
    {
        damage *= 0.95f;
    }
}