using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public float topY = 5f;

    private bool isActive = false;

    private void OnEnable()
    {
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, Space.World);

        if (transform.position.y >= topY)
        {
            Despawn();
        }
    }

    public void Initialize(float speed, float topLimit)
    {
        moveSpeed = speed;
        topY = topLimit;
        isActive = true;
    }

    public void Despawn()
    {
        isActive = false;
        gameObject.SetActive(false);
        ElevatorManager.Instance.RecyclePlatform(this);
    }
}
