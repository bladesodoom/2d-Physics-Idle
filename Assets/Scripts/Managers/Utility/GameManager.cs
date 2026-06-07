using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public SaveData CurrentSave { get; private set; }

    private async void Start()
    {
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // TODO: Implement - Load save data from disk or create a new save if none exists
        await Task.Delay(1000); // Simulate async loading
    }

    public async Task SaveAsync()
    {
        // TODO: Implement - Update CurrentSave with the latest game state before saving
        await Task.Delay(1000); // Simulate async saving
    }

    private void OnApplicationQuit()
    {
        _ = SaveAsync();
    }
}