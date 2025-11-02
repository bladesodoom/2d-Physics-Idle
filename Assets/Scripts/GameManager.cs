using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        CurrencyManager.Instance.Initialize();
        MatterManager.Instance.Initialize();
        PegManager.Instance.Initialize();
        UIManager.Instance.Initialize();
    }
}
