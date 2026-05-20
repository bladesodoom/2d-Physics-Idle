using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Conveyor : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timedLife;

    private float timer;

    private void Start()
    {
        timer = Time.deltaTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timedLife)
        {
            Destroy(gameObject);
        }
    }
}
