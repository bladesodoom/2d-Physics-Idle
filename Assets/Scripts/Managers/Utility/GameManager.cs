using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SaveManager saveManager;

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
        ApplySave(CurrentSave);
    }

    private void ApplySave(SaveData data)
    {
        CurrencyManager.Instance.InitializeStats(data);
        Debug.Log($"[GameManager] Save applied — money: {data.money}  last save: {data.lastSaveTime}");
        ConveyorManager.Instance.InitializeSave(data);
    }


    public async Task SaveAsync()
    {
        CurrentSave.money = CurrencyManager.Instance.currentMoney;

        await saveManager.SaveAsync(CurrentSave);
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            _ = SaveAsync();
    }

    private void OnApplicationQuit()
    {
        _ = SaveAsync();
    }
}