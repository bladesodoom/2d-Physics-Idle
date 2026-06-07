using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float value;
    public float Value { get => value; }

    public void InitializeObstacle(float val = 1, float size = 3)
    {
        value = val;
    }

    public void IncreaseValue()
    {
        value += 1;
    }
}
