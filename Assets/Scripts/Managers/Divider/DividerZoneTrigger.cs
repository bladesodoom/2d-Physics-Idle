using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public class DividerZoneTrigger : MonoBehaviour
{
    private DividerData data;

    public void Setup(DividerData dividerData)
    {
        data = dividerData;

        var col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;

        var eff = GetComponent<PlatformEffector2D>();
        eff.useOneWay = true;
        eff.useOneWayGrouping = true;
        eff.surfaceArc = 180f;
        eff.rotationalOffset = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Matter m = other.GetComponent<Matter>();
        if (m == null) return;

        float reward = m.Value * data.multiplier;
        CurrencyManager.Instance.Add(reward);

        Debug.Log($"[DividerZoneTrigger] Matter landed in zone {data.index} (x{data.multiplier:F2}) -> +{reward:F0}");
    }
}
