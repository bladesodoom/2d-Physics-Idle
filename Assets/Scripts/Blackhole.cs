using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blackhole : MonoBehaviour
{
    [SerializeField] float gravityForce;
    [SerializeField] float gravityRange;

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
            Destroy(other.gameObject);
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
