using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private GameObject conveyorPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform[] waypoints;
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
            SpawnConveyor();
            timer = Time.deltaTime;
        }
    }

    private void SpawnConveyor()
    {
        GameObject newConv = Instantiate(conveyorPrefab, spawnPoint.transform.position, Quaternion.identity);
        Conveyor conveyor = newConv.GetComponent<Conveyor>();
        conveyor.InitializeConv(waypoints);
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
