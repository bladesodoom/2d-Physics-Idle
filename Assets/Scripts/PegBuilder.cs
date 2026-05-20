using UnityEngine;
public class PegBuilder : MonoBehaviour
{
    [SerializeField] private GameObject pegPrefab;
    [SerializeField] private float spacing;
    [SerializeField] private GameObject matterPrefab;

    private GameObject pegBoard;
    private float matterSize;
    private float pegSize;

    private void Start()
    {
        pegBoard = this.gameObject;
        matterSize = matterPrefab.GetComponent<Matter>().Scale;
        pegSize = pegPrefab.GetComponent<Peg>().Scale;
        BuildPegBoard();
    }

    public void BuildPegBoard()
    {
        BoxCollider2D collider = pegBoard.GetComponent<BoxCollider2D>();
        Bounds boardBounds = collider.bounds;

        float boardWidth = boardBounds.size.x;
        float boardHeight = boardBounds.size.y;

        float horizontalSpacing = pegSize + (matterSize * spacing);
        float verticalSpacing = horizontalSpacing * 0.866f;

        int columns = Mathf.Max(1, Mathf.FloorToInt((boardBounds.size.x - pegSize) / horizontalSpacing) + 1);
        int rows = Mathf.Max(1, Mathf.FloorToInt((boardBounds.size.y - pegSize) / verticalSpacing) + 1);

        float totalWidth = (columns - 1) * horizontalSpacing;
        float totalHeight = (rows - 1) * verticalSpacing;

        float startX = boardBounds.min.x + pegSize / 2f;
        float startY = boardBounds.min.y + pegSize / 2f;

        for (int i = 0; i < rows; i++)
        {
            float offsetX = (i % 2 == 1) ? horizontalSpacing / 2f : 0f;

            for (int j = 0; j < columns; j++)
            {
                float posX = startX + j * horizontalSpacing + offsetX;
                float posY = startY + i * verticalSpacing;

                GameObject thisPeg = Instantiate(pegPrefab, pegBoard.transform);
                Peg newPeg = thisPeg.GetComponent<Peg>();
                newPeg.gameObject.transform.position = new Vector3(posX, posY, 0f);
                newPeg.InitializePeg();

                newPeg.gameObject.transform.localScale = new Vector3(pegSize, pegSize, 1);
                newPeg.gameObject.transform.SetParent(this.transform);
                newPeg.gameObject.name = $"Peg_{j}_{i}";
            }
        }
    }
}