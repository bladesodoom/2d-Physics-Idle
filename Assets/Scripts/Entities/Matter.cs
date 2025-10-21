using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Matter : MonoBehaviour
{
    [Header("Physics")]
    public Rigidbody2D rb;
    public bool isConsumed = false;

    [Header("Stat")]
    private float value = 1f;
    private float mass = 1f;
    private float damage = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        isConsumed = false;
    }

    public void Initialize(float val, float dmg)
    {
        value = val;
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Peg peg))
        {
            peg.GainXP();
            peg.TakeDamage(damage);
            value += peg.pegValue;
            CurrencyManager.Instance.AddCurrency(peg.pegValue);
        }
        else if (collision.gameObject.TryGetComponent(out Blackhole blackhole))
        {
            blackhole.Absorb(mass);
            Blackhole.Instance.Absorb(mass);
            CurrencyManager.Instance.AddCurrency(value);
            gameObject.SetActive(false);
            MatterManager.Instance.RecycleMatter(this);
        }
    }
}
