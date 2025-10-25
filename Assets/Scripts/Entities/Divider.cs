using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Divider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider2D dividerCollider;
    [SerializeField] private Collider2D triggerArea;
    [SerializeField] private SpriteRenderer dividerVisual;
    [SerializeField] private DividerData data;

    [Header("Settings")]
    public float areaWidth = 1.5f;
    public float areaHeight = 2.5f;
    public float areaOffset = 1.0f;

    private void Awake()
    {
        if (!dividerCollider)
            dividerCollider = GetComponent<BoxCollider2D>();

        SetupTriggerArea();
    }

    private void SetupTriggerArea()
    {
        if (triggerArea == null)
        {
            GameObject triggerObj = new($"{name}_TriggerArea");
            triggerObj.transform.SetParent(transform);
            triggerObj.transform.localPosition = new Vector3(-areaOffset, 0, 0);
            triggerObj.layer = LayerMask.NameToLayer("Triggers");

            BoxCollider2D trigger = triggerObj.AddComponent<BoxCollider2D>();
            trigger.isTrigger = true;
            trigger.size = new Vector2(areaWidth, areaHeight);

            DividerArea area = triggerObj.AddComponent<DividerArea>();
            area.parentDivider = this;
            triggerArea = trigger;
        }
    }
    public void ApplyUpgradeData(DividerData newData)
    {
        data = newData;
    }
}
