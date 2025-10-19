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
        Blackhole.Instance.singularityPoints = data.singularityPoints;
        Blackhole.Instance.collapseCount = data.collapseCount;
        Blackhole.Instance.collapseMassScaler = data.collapseMassScaler;
        Blackhole.Instance.spConversionRate = data.spConversionRate;

        CurrencyManager.Instance.currentCurrency = data.currency;

        MatterManager.Instance.maxActiveMatter = data.maxMatter;
        MatterManager.Instance.spawnInterval = data.spawnInterval;
        MatterManager.Instance.xScale = data.xScale;
        MatterManager.Instance.yScale = data.yScale;
        MatterManager.Instance.currentValue = data.value;
        MatterManager.Instance.damage = data.damage;

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
        saveData.singularityPoints = Blackhole.Instance.singularityPoints;
        saveData.collapseCount = Blackhole.Instance.collapseCount;
        saveData.collapseMassScaler = Blackhole.Instance.collapseMassScaler;
        saveData.spConversionRate = Blackhole.Instance.spConversionRate;

        saveData.currency = CurrencyManager.Instance.currentCurrency;

        saveData.maxMatter = MatterManager.Instance.maxActiveMatter;
        saveData.spawnInterval = MatterManager.Instance.spawnInterval;
        saveData.xScale = MatterManager.Instance.xScale;
        saveData.yScale = MatterManager.Instance.yScale;
        saveData.value = MatterManager.Instance.currentValue;
        saveData.damage = MatterManager.Instance.damage;

        saveData.pegDataList = PegManager.Instance.GetAllPegData();

        SaveSystem.Save(saveData);
    }

    private void OnApplicationQuit()
    {
        GameData saveData = new GameData();

        saveData.currentMass = Blackhole.Instance.currentMass;
        saveData.singularityPoints = Blackhole.Instance.singularityPoints;
        saveData.collapseCount = Blackhole.Instance.collapseCount;
        saveData.collapseMassScaler = Blackhole.Instance.collapseMassScaler;
        saveData.spConversionRate = Blackhole.Instance.spConversionRate;

        saveData.currency = CurrencyManager.Instance.currentCurrency;

        saveData.maxMatter = MatterManager.Instance.maxActiveMatter;
        saveData.spawnInterval = MatterManager.Instance.spawnInterval;
        saveData.xScale = MatterManager.Instance.xScale;
        saveData.yScale = MatterManager.Instance.yScale;
        saveData.value = MatterManager.Instance.currentValue;
        saveData.damage = MatterManager.Instance.damage;

        saveData.pegDataList = PegManager.Instance.GetAllPegData();

        SaveSystem.Save(saveData);
    }
}
