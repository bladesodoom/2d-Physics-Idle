using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Matter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private MatterData data;

    public void Initialize(MatterData data)
    {
        this.data = data;
        transform.localScale = Vector3.one * data.scale;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        ApplyRandomForce();
    }

    private void ApplyRandomForce()
    {
        Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), -1f).normalized;
        rb.AddForce(randomDir * 2f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Peg"))
        {
            Peg peg = other.GetComponent<Peg>();
            if (peg != null)
            {
                peg.TakeDamage(data.damage);
                CurrencyManager.Instance.AddCurrency(data.baseValue);
            }

            MatterManager.Instance.RemoveMatter(this);
        }
    }
}
