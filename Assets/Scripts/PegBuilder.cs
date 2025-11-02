using UnityEngine;
public class PegBuilder : MonoBehaviour
{
    // TODO: Automatically calculate spacing ensuring first and last columns and rows pegs touch the edge of the pegBoards boxcollider bounds
    /* This could be done by specifying a number of rows and columns and having a failsafe to ensure the spacing between the edges of the pegs
     * doesn't get smaller than like 125% of the matter size */
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

                GameObject newPeg = Instantiate(pegPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
                Peg peg = newPeg.GetComponent<Peg>();
                peg.InitializePeg();

                newPeg.transform.localScale = new Vector3(pegSize, pegSize, 1);
                newPeg.transform.SetParent(this.transform);
                newPeg.name = $"Peg_{j}_{i}";
            }
        }
    }
}