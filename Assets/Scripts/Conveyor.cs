using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Conveyor : MonoBehaviour
{
    [SerializeField] private Transform conveyorPivot;

    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotationSpeed = 8;

    private Transform[] transforms;
    private int pointIndex = 0;

    public void InitializeConv(Transform[] waypoints)
    {
        transforms = waypoints;
    }
    private void Update()
    {
        if (pointIndex < transforms.Length)
        {
            FollowPoints();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void FollowPoints()
    {
        Vector3 targetPos = transforms[pointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1)
        {
            pointIndex++;
        }
    }
}
