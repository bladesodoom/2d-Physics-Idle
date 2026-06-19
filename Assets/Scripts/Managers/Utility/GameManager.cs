using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] private SaveManager saveManager;

    public SaveData CurrentSave { get; private set; }

    private async void Start()
    {
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        CurrentSave = await saveManager.LoadAsync();
        ApplySave(CurrentSave);
    }

    private void ApplySave(SaveData saveData)
    {
        BallManager.Instance.InitializeSave(saveData);
        ConveyorManager.Instance.InitializeSave(saveData);
        DropperManager.Instance.InitializeSave(saveData);
        MultiplierManager.Instance.InitializeSave(saveData);
        ObstacleManager.Instance.InitializeSave(saveData);
        CurrencyManager.Instance.InitializeSave(saveData);
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