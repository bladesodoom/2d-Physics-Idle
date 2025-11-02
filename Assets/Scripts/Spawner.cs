using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform objectSpawnPoint;

    private float randomInterval;
    private float timer;

    public void Start()
    {
        timer = Time.deltaTime;
        randomInterval = GetRandomInterval();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > randomInterval)
        {
            SpawnObject();
            timer = Time.deltaTime;
            randomInterval = GetRandomInterval();
        }
    }

    private float GetRandomInterval()
    {
        return Random.Range(1, 5);
    }

    private void SpawnObject()
    {
        GameObject obj = Instantiate(objectToSpawn, objectSpawnPoint);
    }
}