using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [Header("기본 아이템")]
    public GameObject applePrefab;
    public GameObject bombPrefab;

    [Header("특수 아이템")]
    public GameObject goldenApplePrefab;
    public GameObject starPrefab;
    public GameObject heartPrefab;

    [Header("생성 설정")]
    public float span = 0.8f;
    public int bombRatio = 3;
    public float minSpeed = 2f;
    public float maxSpeed = 3f;

    [Header("레벨 설정")]
    public float levelUpTime = 8f;
    public float speedIncrement = 0.5f;
    public float spanDecrement = 0.1f;
    public float minSpan = 0.2f;

    float delta = 0f;
    float levelTimer = 0f;
    int currentLevel = 1;
    bool isGameOver = false;

    GameObject director;

    void Start()
    {
        director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (isGameOver) return;

        levelTimer += Time.deltaTime;
        if (levelTimer >= levelUpTime)
        {
            levelTimer = 0f;
            LevelUp();
        }

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

        if (dice < 10 && starPrefab != null)
        {
            item = Instantiate(starPrefab);
            item.tag = "Star";
        }
        else if (dice < 18 && heartPrefab != null)
        {
            item = Instantiate(heartPrefab);
            item.tag = "Heart";
        }
        else if (dice < 33 && goldenApplePrefab != null)
        {
            item = Instantiate(goldenApplePrefab);
            item.tag = "GoldenApple";
        }
        else if (dice < 73)
        {
            item = Instantiate(bombPrefab);
            item.tag = "Bomb";
        }
        else
        {
            item = Instantiate(applePrefab);
            item.tag = "Apple";
        }

        float x = Random.Range(-1, 2);
        float z = Random.Range(-1, 2);
        item.transform.SetParent(transform);
        item.transform.position = new Vector3(x, 7, z);

        ItemController ic = item.GetComponent<ItemController>();
        if (ic != null)
            ic.SetSpeed(Random.Range(minSpeed, maxSpeed));
    }

    void LevelUp()
    {
        currentLevel++;
        minSpeed += speedIncrement;
        maxSpeed += speedIncrement;
        span = Mathf.Max(minSpan, span - spanDecrement);

        if (currentLevel % 3 == 0 && bombRatio < 5)
            bombRatio++;

        if (director != null)
            director.GetComponent<GameDirector>().OnLevelUp(currentLevel);
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }
}