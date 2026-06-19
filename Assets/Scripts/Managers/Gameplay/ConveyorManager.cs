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

    protected override void InitializeStats()
    {
        conveyorSpeed = base.SaveData.conveyorSpeed;
        conveyorSpawnRate = base.SaveData.conveyorSpawnRate;
    }

    private void Update()
    {
        if (conveyorSpawnRate <= 0f)
            return;
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
