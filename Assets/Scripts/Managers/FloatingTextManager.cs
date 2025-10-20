using System.Collections;

using TMPro;

using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    public GameObject floatingTextPrefab;

    private Color textColor = new Color(255, 255, 255);

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

    public void ShowFloatingText(Vector3 worldPosition, string text)
    {
        if (floatingTextPrefab == null) return;

        GameObject instance = Instantiate(floatingTextPrefab, worldPosition, Quaternion.identity, transform);
        TMP_Text tmp = instance.GetComponentInChildren<TMP_Text>();
        CanvasGroup cg = instance.GetComponent<CanvasGroup>();

        if (tmp != null)
        {
            tmp.text = text;
            tmp.color = textColor;
        }

        StartCoroutine(FadeAndMove(instance, cg));
    }

    private IEnumerator FadeAndMove(GameObject obj, CanvasGroup cg)
    {
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 2f, 0);

        float duration = 0.8f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
            if (cg != null) cg.alpha = 1f - t;

            yield return null;
        }

        Destroy(obj);
    }
}
