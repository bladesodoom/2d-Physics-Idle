using System.Collections.Generic;

using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public static ConveyorManager Instance { get; private set; }

    [Header("References")]
    public GameObject conveyorPrefab;
    public Transform blackholeCenter;

    [Header("Settings")]
    public float offsetFromHole = 2f;
    public int conveyorsPerSide = 1;

    private List<Conveyor> allConveyors = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SpawnConveyors();
    }

    public void SpawnConveyors()
    {
        ClearConveyors();

        // CreateConveyor(position, side);
    }

    private void CreateConveyor(Vector2 position, Conveyor.ConveyorSide side)
    {
        GameObject obj = Instantiate(conveyorPrefab, position, Quaternion.identity, transform);
        Conveyor conveyor = obj.GetComponent<Conveyor>();
        conveyor.side = side;
        allConveyors.Add(conveyor);
    }

    public void ClearConveyors()
    {
        foreach (var c in allConveyors)
            if (c != null) Destroy(c.gameObject);
        allConveyors.Clear();
    }
}
