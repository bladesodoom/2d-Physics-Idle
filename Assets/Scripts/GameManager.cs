using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject obstacleBuilder;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        Initializer();
    }

    private void Initializer()
    {
        ObstacleBuilder builder = obstacleBuilder.GetComponent<ObstacleBuilder>();
        builder.DoInitialize();
    }
}
