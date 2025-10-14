using UnityEngine;

public class Pusher : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float leftLimit = -5f;
    public float rightLimit = 5f;

    public bool movingRight = true;

    private bool isActive = false;

    private void OnEnable()
    {
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        float direction = movingRight ? 1f : -1f;

        transform.Translate(Vector2.right * moveSpeed * direction * Time.deltaTime, Space.World);

        if (movingRight && transform.position.x >= rightLimit)
        {
            Despawn();
        }
        else if (!movingRight && transform.position.x <= leftLimit)
        {
            Despawn();
        }
    }

    public void Initialize(float speed, bool directionRight, float leftBound, float rightBound)
    {
        moveSpeed = speed;
        movingRight = directionRight;
        leftLimit = leftBound;
        rightLimit = rightBound;
        isActive = true;
    }

    public void Despawn()
    {
        isActive = false;
        gameObject.SetActive(false);
        PusherManager.Instance.RecyclePusher(this);
    }
}
