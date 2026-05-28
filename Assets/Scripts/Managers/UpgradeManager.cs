using UnityEngine;

public abstract class UpgradeManager<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected SaveData SaveData;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
    }

    public virtual void InitializeSave(SaveData saveData)
    {
        SaveData = saveData;
        InitializeStats();
    }
    protected virtual void InitializeStats()
    {
        // TODO: Implement - This method should be overridden by derived classes
        // to initialize their specific stats.
    }

    protected virtual void WriteToSave()
    {

    }
}
