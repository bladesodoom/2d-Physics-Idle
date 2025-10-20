using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AcceleratorArea : MonoBehaviour
{
    [Header("Force Power")]
    public float impulseForce = 5f;
    public float stayForce = 80f;

    public bool isPushRight = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Matter"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            Vector2 direction = isPushRight ? Vector2.right : Vector2.left;
            rb.AddForce(direction * impulseForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Matter"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            Vector2 direction = isPushRight ? Vector2.right : Vector2.left;
            rb.AddForce(direction * stayForce * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }
}