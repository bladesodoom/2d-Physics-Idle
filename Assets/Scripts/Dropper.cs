using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dropper : MonoBehaviour
{
    [SerializeField] private GameObject matterParent;
    [SerializeField] private GameObject matterPrefab;

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
            SpawnMatter();
            timer = Time.deltaTime;
        }
    }

    private void SpawnMatter()
    {
        Vector3 spawnPositon = GetRandomPosition();
        GameObject newMatter = Instantiate(matterPrefab, spawnPositon, Quaternion.identity);
        newMatter.transform.SetParent(matterParent.transform);
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
