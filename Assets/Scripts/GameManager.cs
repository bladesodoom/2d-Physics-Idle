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
        DontDestroyOnLoad(Instance);
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
        // TODO: Implement
    }

    public async Task SaveAsync()
    {
        // Collect all current game state into CurrentSave here
        await saveManager.SaveAsync(CurrentSave);
    }

    private void OnApplicationQuit()
    {
        _ = SaveAsync();
    }
}