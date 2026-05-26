using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObjectDropper : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;

    private GameObject dropper;

    [Header("Dropper Stats")]
    [SerializeField] private float spawnRate;

    private float timer;

    private void Start()
    {
        dropper = this.gameObject;
        timer = Time.deltaTime;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObject();
            timer = Time.deltaTime;
        }
    }

    private void SpawnObject()
    {
        Vector3 spawnPositon = GetRandomPosition();
        GameObject newObject = Instantiate(objectPrefab, spawnPositon, Quaternion.identity);
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
