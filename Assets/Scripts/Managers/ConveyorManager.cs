using UnityEngine;

public class ConveyorManager : UpgradeManager<ConveyorManager>
{
    [SerializeField] private GameObject conveyorPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform leftDespawn;
    [SerializeField] private Transform rightDespawn;

    private float ConveyorSpeed;
    private float ConveyorSpawnRate;

    private float spawnTimer;

    public override void InitializeSave(SaveData saveData)
    {
        ConveyorSpeed = saveData.conveyorSpeed;
        ConveyorSpawnRate = saveData.conveyorSpawnRate;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= ConveyorSpawnRate)
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
        conveyor.Initialize(ConveyorSpeed, moveLeft, despawnPoint);
    }
}
