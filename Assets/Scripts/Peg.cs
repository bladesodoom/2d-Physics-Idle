using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Peg : MonoBehaviour
{
    [Header("Stats")]
    [Range(0.5f, 5)][SerializeField] private float scale = 3;
    [SerializeField] private float value = 1;
    [SerializeField] private float health = 25;
    public float Scale { get => scale; }
    public float Value { get => value; }
    public float Health { get => health; }

    public void InitializePeg(float val = 1, float size = 3, float hp = 25)
    {
        value = val;
        scale = size;
        health = hp;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Matter"))
        {
            float damage = other.gameObject.GetComponent<Matter>().Damage;
            TakeDamage(damage);
            CurrencyManager.Instance.AddCurrency(Value, CurrencyManager.CurrencyType.M);
        }
    }

    private void TakeDamage(float amount)
    {
        health -= amount;
        // Spawn floating text with red color negative y at collision point
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseValue()
    {
        value += 1;
    }

    public void DecreaseSize()
    {
        scale *= 0.99f;
    }

    public void IncreaseHealth()
    {
        health *= 1.15f;
    }
}
