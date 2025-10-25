using TMPro;

using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FloatingText : MonoBehaviour
{
    private TMP_Text textMesh;
    private float lifetime;
    private float speed;
    private float elapsedTime;
    private float directionY;
    private Color startColor;

    public void Initialize(string text, Color color, float directionY, float duration, float moveSpeed)
    {
        if (textMesh == null)
            textMesh = GetComponent<TMP_Text>();

        textMesh.text = text;
        textMesh.color = color;
        startColor = color;

        this.lifetime = duration;
        this.speed = moveSpeed;
        this.directionY = Mathf.Sign(directionY);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        transform.position += Vector3.up * directionY * speed * Time.deltaTime;

        float fade = 1f - (elapsedTime / lifetime);
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, fade);

        if (elapsedTime >= lifetime)
            Destroy(gameObject);
    }
}
