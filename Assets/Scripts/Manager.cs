using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"[Manager] Duplicate instance of {typeof(T).Name} detected, destroying the new one.");
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
    }

    public virtual void Initialize() { }
}
