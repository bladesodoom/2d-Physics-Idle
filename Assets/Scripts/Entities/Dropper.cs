using UnityEngine;

public class Dropper : MonoBehaviour
{
    [Header("Runtime Stats")]
    public float spawnInterval;
    public int spawnCount;
    public float cooldownTime;

    private float cooldownTimer;

    public void ApplyTierStats(DropperData data)
    {
        spawnInterval = data.spawnInterval;
        spawnCount = data.spawnCount;
        cooldownTime = data.cooldownTime;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            ActivateDropper();
            cooldownTimer = cooldownTime;
        }
    }

    private void ActivateDropper()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            MatterManager.Instance.SpawnMatter();
        }

        Debug.Log($"[Dropper] Dropped {spawnCount} Matter(s) at efficiency {efficiency}");
    }
}
