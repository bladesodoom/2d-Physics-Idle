using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class SaveManager : Manager<SaveManager>
{
    [Header("Cloud Save")]
    [SerializeField] private bool useCloudSave = false;

    private const string FILE_NAME = "save.json";
    private const string CLOUD_KEY = "save_data";

    private string LocalPath => Path.Combine(Application.persistentDataPath, FILE_NAME);

    public async Task<SaveData> LoadAsync()
    {
        if (useCloudSave)
        {
            SaveData cloudData = await LoadFromCloudAsync();
            if (cloudData != null)
            {
                Debug.Log("[SaveManager] Loaded from cloud.");
                return cloudData;
            }
        }

        SaveData localData = LoadFromDisk();
        if (localData != null)
        {
            Debug.Log("[SaveManager] Loaded from local file.");
            return localData;
        }

        Debug.Log("[SaveManager] No save found. Starting new game.");
        return SaveData.NewGame();
    }

    public async Task SaveAsync(SaveData data)
    {
        data.lastSaveTime = DateTime.UtcNow.ToString("o");

        SaveToDisk(data);

        if (useCloudSave)
            await SaveToCloudAsync(data);
    }

    private SaveData LoadFromDisk()
    {
        if (!File.Exists(LocalPath))
            return null;

        try
        {
            string json = File.ReadAllText(LocalPath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveManager] Failed to read local save: {e.Message}");
            return null;
        }
    }

    private void SaveToDisk(SaveData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(LocalPath, json);
            Debug.Log($"[SaveManager] Saved locally → {LocalPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveManager] Failed to write local save: {e.Message}");
        }
    }

    private async Task<SaveData> LoadFromCloudAsync()
    {
        
         #if CLOUD_SAVE
         try
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            var result = await CloudSaveService.Instance.Data.Player
                             .LoadAsync(new HashSet<string> { CLOUD_KEY });

            if (result.TryGetValue(CLOUD_KEY, out var item))
                return JsonUtility.FromJson<SaveData>(item.Value.GetAsString());
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[SaveManager] Cloud load failed: {e.Message}");
        }
        #endif

        return null;
    }

    private async Task SaveToCloudAsync(SaveData data)
    {
         #if CLOUD_SAVE
         try
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            string json = JsonUtility.ToJson(data);
            var payload = new Dictionary<string, object> { { CLOUD_KEY, json } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(payload);
            Debug.Log("[SaveManager] Saved to cloud.");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[SaveManager] Cloud save failed: {e.Message}");
        }
        #endif

    }
}