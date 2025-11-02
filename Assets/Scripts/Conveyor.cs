using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Conveyor : MonoBehaviour
{
    // Calculate rotation speed based on move speed and distance between the specifc indexs of waypoints
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200;

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
            StartCoroutine(DespawnDelay());
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

    private IEnumerator DespawnDelay()
    {
        yield return new WaitForSeconds(1);
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
}
