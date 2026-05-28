using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float moveSpeed {  get; private set; }
    public bool isMovingLeft { get; private set; }
    public Transform despawnPoint { get; private set; }

    public void Initialize(float moveSpeed, bool direction, Transform despawn)
    {
        this.moveSpeed = moveSpeed;
        this.isMovingLeft = direction;
        this.despawnPoint = despawn;
    }

    public void Update()
    {
        if (isMovingLeft && this.transform.position.x > despawnPoint.transform.position.x)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (!isMovingLeft && this.transform.position.x < despawnPoint.transform.position.x)
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }
}
