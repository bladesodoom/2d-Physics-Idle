using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dropper : MonoBehaviour
{
    [SerializeField] private GameObject matterParent;
    [SerializeField] private GameObject matterPrefab;

    private GameObject dropper;

    [Header("Dropper Stats")]
    private float spawnRate = 5;

    private float timer;

    private void Start()
    {
        dropper = this.gameObject;
        timer = Time.deltaTime;
    }
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnMatter();
            timer = Time.deltaTime;
        }
    }

    private void SpawnMatter()
    {
        GameObject newMatter = Instantiate(matterPrefab, dropper.transform.position, Quaternion.identity);
        newMatter.transform.SetParent(matterParent.transform);
    }

}
