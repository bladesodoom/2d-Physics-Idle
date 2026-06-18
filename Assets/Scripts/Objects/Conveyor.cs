using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float moveSpeed {  get; private set; }
    public bool isMovingLeft { get; private set; }
    public Transform despawnPoint { get; private set; }

    public void InitializeConveyor(float moveSpeed, bool direction, Transform despawn)
    {
        this.moveSpeed = moveSpeed;
        this.isMovingLeft = direction;
        this.despawnPoint = despawn;
    }

    public void Update()
    {
        Move();
        CheckDespawn();
    }

    private void Move()
    {
        float direction = isMovingLeft ? -1f : 1f;
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void CheckDespawn()
    {
        if (isMovingLeft)
        {
            if (transform.position.x <= despawnPoint.position.x)
            {
                Despawn();
            }
        }
        else
        {
            if (transform.position.x >= despawnPoint.position.x)
            {
                Despawn();
            }
        }
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }
}
