using UnityEngine;

public class MatterManager : Manager<MatterManager>
{
    [SerializeField] private Matter matterPrefab;
    [SerializeField] private int initialPoolSize = 50;

    private ObjectPool<Matter> pool;

    public override void Initialize()
    {
        base.Initialize();
        pool = new ObjectPool<Matter>(matterPrefab, initialPoolSize, transform);
    }

    public void Despawn(Matter matter)
    {
        pool.ReturnToPool(matter);
    }
}
