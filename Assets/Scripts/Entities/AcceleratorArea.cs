using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AcceleratorArea : MonoBehaviour
{
    [Header("Force Power")]
    public float minPulseForce = 5f;
    public float maxPulseForce = 10f;
    public float pulseDelay = 1f;

    private float pulseTimer;

    public bool isPushRight = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Matter"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            StartCoroutine(DelayedPulse(rb));
        }
    }

    private IEnumerator DelayedPulse(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(pulseDelay);

        if (rb == null) yield break;

        float randomForce = Random.Range(minPulseForce, maxPulseForce);
        Vector2 force = (isPushRight ? Vector2.right : Vector2.left) * randomForce;

        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
