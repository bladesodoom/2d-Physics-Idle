using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Launcher : MonoBehaviour
{
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float delay = 2;
    [SerializeField] private bool isShootingRight = false;

    private GameObject launcher;

    private void Start()
    {
        launcher = this.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Launch(collision));

    }

    private IEnumerator Launch(Collider2D collision)
    {
        yield return new WaitForSeconds(delay);
        Vector2 direction = -transform.right;

        if (isShootingRight)
        {
            direction = transform.right;
        }
        collision.attachedRigidbody.AddForce(direction * launchForce, ForceMode2D.Impulse);
    }
}
