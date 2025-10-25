using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DividerArea : MonoBehaviour
{
    [HideInInspector] public Divider parentDivider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Matter>(out var matter))
        {
            other.attachedRigidbody.linearVelocity = Vector3.zero;
        }
    }
}
