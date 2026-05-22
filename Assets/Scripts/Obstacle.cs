using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    [Header("Stats")]
    [Range(0.5f, 5)][SerializeField] private float scale;
    [SerializeField] private float value;
    public float Scale { get => scale; }
    public float Value { get => value; }

    public void InitializeObstacle(float val = 1, float size = 3)
    {
        value = val;
        scale = size;
    }

    public void IncreaseValue()
    {
        value += 1;
    }

    public void DecreaseSize()
    {
        scale *= 0.99f;
    }
}
