using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float autoSaveInterval = 10;

    private float autoSaveTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameData data = SaveSystem.Load();

        Blackhole.Instance.currentMass = data.currentMass;

        CurrencyManager.Instance.currentCurrency = data.currency;

        MatterManager.Instance.maxActiveMatter = data.maxMatter;
        MatterManager.Instance.spawnInterval = data.spawnInterval;
        MatterManager.Instance.matterScale = data.scale;
        MatterManager.Instance.currentValue = data.value;
        MatterManager.Instance.matterDamage = data.damage;

        PegManager.Instance.GeneratePegGrid(data.pegDataList);
    }

    private void Update()
    {
        autoSaveTimer += Time.deltaTime;

        if (autoSaveTimer >= autoSaveInterval)
        {
            AutoSave();
            autoSaveTimer = Time.deltaTime;
        }
    }

    private void AutoSave()
    {
        GameData saveData = new GameData();

        saveData.currentMass = Blackhole.Instance.currentMass;

        saveData.currency = CurrencyManager.Instance.currentCurrency;

        saveData.maxMatter = MatterManager.Instance.maxActiveMatter;
        saveData.spawnInterval = MatterManager.Instance.spawnInterval;
        saveData.scale = MatterManager.Instance.matterScale;
        saveData.value = MatterManager.Instance.currentValue;
        saveData.damage = MatterManager.Instance.matterDamage;

        saveData.pegDataList = PegManager.Instance.GetAllPegData();

        SaveSystem.Save(saveData);
    }

    private void OnApplicationQuit()
    {
        GameData saveData = new GameData();

        saveData.currentMass = Blackhole.Instance.currentMass;

        saveData.currency = CurrencyManager.Instance.currentCurrency;

        saveData.maxMatter = MatterManager.Instance.maxActiveMatter;
        saveData.spawnInterval = MatterManager.Instance.spawnInterval;
        saveData.scale = MatterManager.Instance.matterScale;
        saveData.value = MatterManager.Instance.currentValue;
        saveData.damage = MatterManager.Instance.matterDamage;

        saveData.pegDataList = PegManager.Instance.GetAllPegData();

        SaveSystem.Save(saveData);
    }
}
