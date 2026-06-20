using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dropper : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    private GameObject dropper;

    private float timer;

    private void Start()
    {
        dropper = this.gameObject;
        timer = Time.deltaTime;
    }

    public void Update()
    {
        if (DropperManager.Instance == null || DropperManager.Instance.spawnRate <= 0) return;

        timer += Time.deltaTime;
        if (timer >= DropperManager.Instance.spawnRate)
        {
            if (BallManager.Instance.CanSpawnBall)
                SpawnBall();
            timer = Time.deltaTime;
        }
    }

    private void SpawnBall()
    {
        Vector3 spawnPositon = GetRandomPosition();
        GameObject newObject = Instantiate(ballPrefab, spawnPositon, Quaternion.identity);
        newObject.transform.SetParent(this.transform);
    }

    private Vector3 GetRandomPosition()
    {
        BoxCollider2D collider = dropper.GetComponent<BoxCollider2D>();
        Bounds bounds = collider.bounds;

        float ranX = Random.Range(bounds.min.x, bounds.max.x);
        float ranY = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 spawnPosition = new(ranX, ranY, 1);
        return spawnPosition;
    }
}
