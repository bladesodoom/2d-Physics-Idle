using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hole : MonoBehaviour
{
    private Collider2D holeCollider;

    private void Awake()
    {
        holeCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("FObject"))
        {
            Destroy(other.gameObject);
        }
    }
}
