using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    [Header("Prefab")]
    [SerializeField] private GameObject floatingTextPrefab;

    [Header("Settings")]
    public float defaultDuration = 1.2f;
    public float defaultSpeed = 1.0f;

    [Header("Colors")]
    public Color moneyColor = new(0.2f, 1f, 0.3f);
    public Color hpColor = new(1f, 0.3f, 0.3f);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnMoneyText(Vector3 worldPos, float amount, float directionY = 1f)
    {
        string text = $"+{amount:0}";
        SpawnFloatingText(worldPos, text, moneyColor, directionY);
    }

    public void SpawnHPText(Vector3 worldPos, float amount, float directionY = -1f)
    {
        string text = $"-{amount:0}";
        SpawnFloatingText(worldPos, text, hpColor, directionY);
    }

    public void SpawnFloatingText(Vector3 worldPos, string text, Color color, float directionY)
    {
        if (!floatingTextPrefab)
        {
            Debug.LogWarning("[FloatingTextManager] No floatingTextPrefab assigned!");
            return;
        }

        GameObject obj = Instantiate(floatingTextPrefab, worldPos, Quaternion.identity, transform);
        FloatingText floatingText = obj.GetComponent<FloatingText>();
        if (floatingText == null)
            floatingText = obj.AddComponent<FloatingText>();

        floatingText.Initialize(text, color, directionY, defaultDuration, defaultSpeed);
    }
}
