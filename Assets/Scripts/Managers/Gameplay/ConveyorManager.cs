using UnityEngine;

public class ConveyorManager : UpgradeManager<ConveyorManager>
{
    [SerializeField] private GameObject conveyorPrefab;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform leftDespawn;
    [SerializeField] private Transform rightDespawn;

    private float conveyorSpeed;
    private float conveyorSpawnRate;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= conveyorSpawnRate)
        {
            SpawnConveyor();
            spawnTimer = 0f;
        }
    }

    private void SpawnConveyor()
    {
        GameObject conveyorObj = Instantiate(conveyorPrefab, spawnPoint.position, Quaternion.identity);
        Conveyor conveyor = conveyorObj.GetComponent<Conveyor>();
        bool moveLeft = Random.value > 0.5f;
        Transform despawnPoint = moveLeft ? leftDespawn : rightDespawn;
        conveyor.InitializeOBJ(conveyorSpeed, moveLeft, despawnPoint);
    }
}
