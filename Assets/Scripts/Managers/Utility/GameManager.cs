using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SaveManager saveManager;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private UIManager uiManager;

    public SaveData CurrentSave { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private async void Start()
    {
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        CurrentSave = await saveManager.LoadAsync();
        BuildManagers();
        ApplySave(CurrentSave);
    }

    private void BuildManagers()
    {
        // TODO: Implement - Create all manager instances here, call their DoStart()
        currencyManager.DoStart();
        uiManager.DoStart();
    }

    private void ApplySave(SaveData data)
    {
        // TODO: Implement - Apply loaded save data to the respective managers
        CurrencyManager.Instance.InitializeStats(data.money);
    }

    public async Task SaveAsync()
    {
        // TODO: Implement - Update CurrentSave with the latest game state before saving
        await saveManager.SaveAsync(CurrentSave);
    }

    private void OnApplicationQuit()
    {
        _ = SaveAsync();
    }
}