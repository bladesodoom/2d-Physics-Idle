using System.Collections.Generic;

using UnityEngine;

public class DividerManager : MonoBehaviour
{
    [Header("References")]
    public Transform leftInnerWall;
    public Transform rightInnerWall;

    [Header("Divider Settings")]
    public GameObject dividerPrefab;
    public int dividerCount = 4;
    public float dividerSpacingOffset = 0f;
    public float yPosition = -24.5f;

    public Transform matterPrefab;

    private List<GameObject> dividers = new List<GameObject>();

    private void Start()
    {
        PlaceDividers();
    }

    public void PlaceDividers()
    {
        ClearDividers();

        if (dividerPrefab == null || matterPrefab == null)
        {
            Debug.LogWarning("DividerManager: Missing References.");
            return;
        }
        float matterRadius = GetMatterWorldRadius();
        float dividerHalfWidth = GetObjectWorldRadius(dividerPrefab.gameObject);

        float startPos = leftInnerWall.position.x + dividerHalfWidth + matterRadius + dividerSpacingOffset;
        float endPos = rightInnerWall.position.x - dividerHalfWidth - matterRadius - dividerSpacingOffset;

        PlaceDividers(startPos, endPos, -1);
    }

    private void PlaceDividers(float startX, float endX, int side)
    {
        float totalDistance = Mathf.Abs(endX - startX);
        if (dividerCount <= 0 || totalDistance <= 0f)
        {
            return;
        }

        float step = totalDistance / dividerCount;

        for (int i = 0; i <= dividerCount; i++)
        {
            float x = (side == -1) ? endX - (i * step) : startX + (i * step);

            Vector3 pos = new Vector3(x, yPosition, 0f);
            GameObject divider = Instantiate(dividerPrefab, pos, Quaternion.identity);
            dividers.Add(divider);
            divider.gameObject.transform.SetParent(this.transform);
        }
    }

    private float GetMatterWorldRadius()
    {
        var sr = matterPrefab.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            return (sr.bounds.size.x * 0.5f);
        }
        else
            return matterPrefab.transform.localScale.x * 0.5f;
    }

    private float GetObjectWorldRadius(GameObject obj)
    {
        var sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            return sr.bounds.size.x * 0.5f;
        else
            return obj.transform.localScale.x * 0.5f;
    }

    private void ClearDividers()
    {
        foreach (var d in dividers)
        {
            if (d != null)
                Destroy(d);
        }
        dividers.Clear();
    }
}
