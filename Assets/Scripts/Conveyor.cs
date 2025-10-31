using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Conveyor : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float rotationSpeed = 180;

    private Transform[] waypoints;
    private int pointIndex = 0;

    private Quaternion targetRotation;
    private bool isRotating = false;

    public void InitializeConv(Transform[] points)
    {
        waypoints = points;
        transform.position = waypoints[0].position;
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length < 2)
            return;

        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        if (pointIndex >= waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPos = waypoints[pointIndex + 1].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (IsRotationZone(pointIndex))
        {
            if (!isRotating)
            {
                isRotating = true;
                targetRotation = GetRotationTarget(pointIndex);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            isRotating = false;
        }

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            pointIndex++;
        }
    }

    private bool IsRotationZone(int index)
    {
        return index == 1 || index == 3;
    }

    private Quaternion GetRotationTarget(int index)
    {
        if (index == 1)
            return Quaternion.Euler(0f, 0f, 90f);
        if (index == 3)
            return Quaternion.Euler(0f, 0f, 180f);
        return transform.rotation;
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
