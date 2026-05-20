using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Matter : MonoBehaviour
{
    [Header("Stats")]
    [Range(0.5f, 5)][SerializeField] private float scale;
    [SerializeField] private float value;
    [SerializeField] private float damage;

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
        }
    }

    public void IncreaseValue(float amount)
    {
        value += amount;
    }
}