using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    private float value;
    private float size;

    public float Value { get => value; }
    public float Size { get => size; }

    public void InitializeObstacle(float val = 1, float siz = 3)
    {
        value = val;
        size = siz;
    }

    public void IncreaseValue()
    {
        value += 1;
    }
}
