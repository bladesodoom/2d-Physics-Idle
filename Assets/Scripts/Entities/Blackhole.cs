using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blackhole : MonoBehaviour
{
    public static Blackhole Instance { get; private set; }

    [Header("Visuals & FX")]
    [SerializeField] private float pullForce = 0f;
    [SerializeField] private float pullRadius = 3f;

    private Collider2D holeCollider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        holeCollider = GetComponent<Collider2D>();
        holeCollider.isTrigger = true;
    }

    private void Update()
    {
        ApplyGravitationalPull();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Matter>(out var matter))
        {
            AbsorbMatter(matter);
        }
    }

    private void AbsorbMatter(Matter matter)
    {
        Destroy(matter);
    }

    private void ApplyGravitationalPull()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent<Matter>(out var matter))
            {
                Rigidbody2D rb = matter.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = ((Vector2)transform.position - rb.position).normalized;
                    rb.AddForce(dir * pullForce * Time.deltaTime, ForceMode2D.Force);
                }
            }
        }
    }
}
