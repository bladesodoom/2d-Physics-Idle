using UnityEngine;
public class ObstacleBuilder : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spacing;

    private GameObject obstacleBoard;
    private float obstacleSize;

    private void Start()
    {
        obstacleBoard = this.gameObject;
        obstacleSize = obstaclePrefab.GetComponent<Obstacle>().Scale;
        BuildObstacleBoard();
    }

    public void BuildObstacleBoard()
    {
        BoxCollider2D collider = obstacleBoard.GetComponent<BoxCollider2D>();
        Bounds boardBounds = collider.bounds;

        float boardWidth = boardBounds.size.x;
        float boardHeight = boardBounds.size.y;

        float horizontalSpacing = obstacleSize * spacing;
        float verticalSpacing = horizontalSpacing * 0.866f;

        int columns = Mathf.Max(1, Mathf.FloorToInt((boardBounds.size.x - obstacleSize) / horizontalSpacing) + 1);
        int rows = Mathf.Max(1, Mathf.FloorToInt((boardBounds.size.y - obstacleSize) / verticalSpacing) + 1);

        float totalWidth = (columns - 1) * horizontalSpacing;
        float totalHeight = (rows - 1) * verticalSpacing;

        float startX = boardBounds.min.x + obstacleSize / 2f;
        float startY = boardBounds.min.y + obstacleSize / 2f;

        for (int i = 0; i < rows; i++)
        {
            float offsetX = (i % 2 == 1) ? horizontalSpacing / 2f : 0f;

            for (int j = 0; j < columns; j++)
            {
                float posX = startX + j * horizontalSpacing + offsetX;
                float posY = startY + i * verticalSpacing;

                GameObject thisObstacle = Instantiate(obstaclePrefab, obstacleBoard.transform);
                Obstacle newObstacle = thisObstacle.GetComponent<Obstacle>();
                newObstacle.gameObject.transform.position = new Vector3(posX, posY, 0f);
                newObstacle.InitializeObstacle();

                newObstacle.gameObject.transform.localScale = new Vector3(obstacleSize, obstacleSize, 1);
                newObstacle.gameObject.transform.SetParent(this.transform);
                newObstacle.gameObject.name = $"Obstacle_{j}_{i}";
            }
        }
    }
}