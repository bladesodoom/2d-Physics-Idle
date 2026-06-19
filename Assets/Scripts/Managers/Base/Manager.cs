using UnityEngine;

public abstract class Manager<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected SaveData SaveData;

    protected virtual void Awake()
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
        // This method should be overridden by derived classes
        // to load their specific stats from the SaveData object.
    }

    protected virtual void WriteToSave()
    {
        // This method should be overridden by derived classes
        // to save their specific stats back to the SaveData object.
    }
}
