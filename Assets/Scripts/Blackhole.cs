using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blackhole : MonoBehaviour
{
    [SerializeField] float gravityForce = 10;
    [SerializeField] float gravityRange = 10;

    private Collider2D holeCollider;

    private void Awake()
    {
        holeCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        ApplyGravity();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Matter"))
        {
            Matter matter = other.gameObject.GetComponent<Matter>();
            float amount = matter.Value;
            CurrencyManager.Instance.AddCurrency(amount, CurrencyManager.CurrencyType.M);
            // spawn floating text with the specific color in a positive direction from the collision point
            MatterManager.Instance.Despawn(matter);
        }
    }

    private void ApplyGravity()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravityRange);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent<Matter>(out var matter))
            {
                if (holeCollider.bounds.Contains(col.transform.position))
                    continue;

                Rigidbody2D rb = matter.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = ((Vector2)transform.position - rb.position).normalized;
                    rb.AddForce(dir * gravityForce * Time.deltaTime, ForceMode2D.Force);
                }
            }
        }
    }
}
