using UnityEngine;

public class ObstacleBuilder : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject objectPrefab;

    [SerializeField] private GameObject obstacleParent;

    [Header("Spacing")]
    [SerializeField] private float spacingMultiplier = 1.2f;

    private float obstacleWidth;
    private float obstacleHeight;
    private float objectWidth;
    private float objectHeight;

    public void DoInitialize()
    {
        Vector3 obstacleScale = obstaclePrefab.transform.localScale;
        Vector2 obstacleSprite = obstaclePrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
        obstacleWidth = obstacleSprite.x * obstacleScale.x;
        obstacleHeight = obstacleSprite.y * obstacleScale.y;

        Vector3 objectScale = objectPrefab.transform.localScale;
        Vector2 objectSprite = objectPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
        objectWidth = objectSprite.x * objectScale.x;
        objectHeight = objectSprite.y * objectScale.y;

        BuildObstacles();
    }

    private void BuildObstacles()
    {
        BoxCollider2D boardCollider = obstacleParent.GetComponent<BoxCollider2D>();
        Bounds bounds = boardCollider.bounds;

        float hGap = objectWidth * spacingMultiplier;
        float vGap = objectHeight * spacingMultiplier;

        float stepX = obstacleWidth + hGap;
        float stepY = obstacleHeight + vGap;

        float startX = bounds.min.x + hGap + obstacleWidth * 0.5f;
        float endX = bounds.max.x - hGap - obstacleWidth * 0.5f;

        float startY = bounds.min.y + vGap + obstacleHeight * 0.5f;
        float endY = bounds.max.y - vGap - obstacleHeight * 0.5f;

        int index = 0;
        for (float currentY = startY; currentY <= endY; currentY += stepY)
        {
            for (float currentX = startX; currentX <= endX; currentX += stepX)
            {
                GameObject newObject = Instantiate(
                    obstaclePrefab,
                    new Vector3(currentX, currentY, 0f),
                    Quaternion.identity
                );

                newObject.name = $"Obstacle_{index++}";
            }
        }
    }
}