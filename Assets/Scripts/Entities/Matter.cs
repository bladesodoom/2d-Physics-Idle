using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Matter : MonoBehaviour
{
    [Header("Physics")]
    public Rigidbody2D rb;
    public bool isConsumed = false;

    [Header("Stat")]
    private float value = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        isConsumed = false;
    }

    public void Initialize(float val)
    {
        value = val;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Peg"))
        {
            Peg peg = collision.collider.GetComponent<Peg>();
            if (peg != null)
            {
                peg.GainXP();
                value += peg.currencyValue;
            }
        }
        else if (collision.collider.CompareTag("Blackhole"))
        {
            Blackhole.Instance.Absorb(value);
            gameObject.SetActive(false);
        }
    }
}
