using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Ball : MonoBehaviour
{
    [Header("Stats")]
    public double value { get; private set; }
    private float size;

    private void Start()
    {
        value = BallManager.Instance.ballValue;
        size = BallManager.Instance.ballSize;

        BallManager.Instance.RegisterBall();
    }

    private void OnDestroy()
    {
        BallManager.Instance.UnregisterBall();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
            IncreaseValue(obstacle.Value);
        }
    }

    public void IncreaseValue(double amount) => value += amount;
    public void DecreaseValue(double amount) => value -= amount;
}