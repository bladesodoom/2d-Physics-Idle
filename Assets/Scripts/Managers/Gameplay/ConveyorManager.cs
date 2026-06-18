using UnityEngine;

public class ConveyorManager : UpgradeManager<ConveyorManager>
{
    [SerializeField] private GameObject conveyorPrefab;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform leftDespawn;
    [SerializeField] private Transform rightDespawn;

    public float conveyorSpeed {get; private set;}
    public float conveyorSpawnRate { get; private set; }

    private float spawnTimer;

    public void InitializeStats(SaveData saveData)
    {
        conveyorSpeed = saveData.conveyorSpeed;
        conveyorSpawnRate = saveData.conveyorSpawnRate;
    }

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
        conveyor.InitializeConveyor(conveyorSpeed, moveLeft, despawnPoint);
    }
}
