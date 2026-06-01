using UnityEngine;

public class ItemGenerator2 : MonoBehaviour
{
    [Header("위험 아이템")]
    public GameObject bombPrefab;

    [Header("좋은 아이템")]
    public GameObject applePrefab;
    public GameObject heartPrefab;

    [Header("생성 설정")]
    public float span = 1f;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;

    float delta = 0f;
    bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        GameObject item = null;
        int dice = Random.Range(0, 100);

        if (dice < 40 && bombPrefab != null)
        {
            item = Instantiate(bombPrefab);
            item.tag = "Bomb";
        }
        else if (dice < 55 && heartPrefab != null)
        {
            item = Instantiate(heartPrefab);
            item.tag = "Heart";
        }
        else if (applePrefab != null)
        {
            item = Instantiate(applePrefab);
            item.tag = "Apple";
        }

        if (item == null) return;

        float x = Random.Range(-1, 2);
        float z = Random.Range(-1, 2);
        item.transform.SetParent(transform);
        item.transform.position = new Vector3(x, -0.3f, z);

        ItemController2 ic = item.GetComponent<ItemController2>();
        if (ic != null)
            ic.SetSpeed(Random.Range(minSpeed, maxSpeed));
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }
}