using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private GameObject conveyorPrefab;

    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    [SerializeField] private float spawnRate;

    private float timer;

    private void Start()
    {
        timer = Time.deltaTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnConveyors();
            timer = Time.deltaTime;
        }
    }

    private void SpawnConveyors()
    {
        GameObject lConveyor = Instantiate(conveyorPrefab, leftSpawn.position, Quaternion.identity);
        GameObject rConveyor = Instantiate(conveyorPrefab, rightSpawn.position, Quaternion.identity);
    }
}
