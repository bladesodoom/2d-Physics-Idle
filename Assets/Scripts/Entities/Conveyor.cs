using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public enum ConveyorSide { Left, Right }

    [Header("Settings")]
    public ConveyorSide side;
    public float moveSpeed = 2f;
    public float pivotSpeed = 180f;

    [Header("References")]
    public ConveyorData data;
    public Transform pivotPointTop;
    public Transform pivotPointSide;
    public Transform triggerZone;

    private enum ConveyorState { Moving, Pivoting }
    private ConveyorState state = ConveyorState.Moving;

    private Vector2 direction;
    private Rect boardBounds;

    private Vector2 targetPosition;
    private Quaternion targetRotation;
    private bool movingVertical = false;
    private bool movingUp = true;

    private void Start()
    {
        if (data != null)
            moveSpeed = data.moveSpeed;

        boardBounds = ConveyorManager.Instance.BoardBounds;
        InitializeDirection();
    }

    private void Update()
    {
        switch (state)
        {
            case ConveyorState.Moving:
                MoveAlongPath();
                break;
            case ConveyorState.Pivoting:
                RotateAtCorner();
                break;
        }
    }

    private void InitializeDirection()
    {
        if (side == ConveyorSide.Right)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        targetPosition = GetNextTarget();
        transform.rotation = Quaternion.Euler(0f, 0f, direction.x > 0 ? 0f : 180f);
    }

    private void MoveAlongPath()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
        {
            state = ConveyorState.Pivoting;
            SetupNextPivot();
        }
    }

    private void SetupNextPivot()
    {
        Transform pivot = pivotPointTop != null ? pivotPointTop : transform;

        if (!movingVertical)
        {
            targetRotation = Quaternion.Euler(0f, 0f, 90f);
            movingVertical = true;
        }
        else
        {
            float rotZ = (direction.x > 0) ? 180f : 0f;
            targetRotation = Quaternion.Euler(0f, 0f, rotZ);
            movingVertical = false;
            direction *= -1f;
        }
    }

    private void RotateAtCorner()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, pivotSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            state = ConveyorState.Moving;
            targetPosition = GetNextTarget();
        }
    }

    private Vector2 GetNextTarget()
    {
        if (!movingVertical)
        {
            float xTarget = (side == ConveyorSide.Right) ? boardBounds.xMax - 1f : boardBounds.xMin + 1f;
            return new Vector2(xTarget, transform.position.y);
        }
        else
        {
            float yTarget = boardBounds.yMax - 1f;
            return new Vector2(transform.position.x, yTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPosition, 0.1f);
    }
}
