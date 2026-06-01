using UnityEngine;

public class BasketController : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip appleSE;
    public AudioClip bombSE;
    public AudioClip specialSE;

    [Header("Effect")]
    public GameObject appleEffect;
    public GameObject bombEffect;
    public GameObject specialEffect;

    GameObject director;
    AudioSource aud;
    bool isGameOver = false;
    bool isAutoPilot = false;
    float autoPilotTimer = 0f;
    Transform targetApple = null;

    void Start()
    {
        Application.targetFrameRate = 60;
        aud = GetComponent<AudioSource>();
        director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (isGameOver) return;

        // 자동조종 모드
        if (isAutoPilot)
        {
            autoPilotTimer -= Time.deltaTime;
            if (autoPilotTimer <= 0f)
            {
                isAutoPilot = false;
                return;
            }

            // 가장 가까운 사과 찾기
            GameObject[] apples = GameObject.FindGameObjectsWithTag("Apple");
            float minDist = float.MaxValue;
            targetApple = null;

            foreach (GameObject apple in apples)
            {
                float dist = Vector3.Distance(transform.position, apple.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    targetApple = apple.transform;
                }
            }

            // 사과 쪽으로 이동
            if (targetApple != null)
            {
                Vector3 targetPos = new Vector3(
                    Mathf.RoundToInt(targetApple.position.x),
                    0,
                    Mathf.RoundToInt(targetApple.position.z)
                );
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    5f * Time.deltaTime
                );
            }
            return;
        }

        // 일반 조작
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 0, z);
            }
        }
    }

    public void StartAutoPilot(float duration)
    {
        isAutoPilot = true;
        autoPilotTimer = duration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        Debug.Log("충돌감지! 태그: " + other.gameObject.tag);

        GameDirector gd = director.GetComponent<GameDirector>();

        if (other.gameObject.tag == "Apple")
        {
            if (appleSE != null) aud.PlayOneShot(appleSE);
            SpawnEffect(appleEffect, other.transform.position);
            gd.GetApple();
        }
        else if (other.gameObject.tag == "Bomb")
        {
            if (bombSE != null) aud.PlayOneShot(bombSE);
            SpawnEffect(bombEffect, other.transform.position);
            gd.GetBomb();
        }
        else if (other.gameObject.tag == "GoldenApple")
        {
            if (specialSE != null) aud.PlayOneShot(specialSE);
            SpawnEffect(specialEffect, other.transform.position);
            gd.GetGoldenApple();
        }
        else if (other.gameObject.tag == "Star")
        {
            if (specialSE != null) aud.PlayOneShot(specialSE);
            SpawnEffect(specialEffect, other.transform.position);
            gd.GetStar();
        }
        else if (other.gameObject.tag == "Heart")
        {
            if (specialSE != null) aud.PlayOneShot(specialSE);
            SpawnEffect(specialEffect, other.transform.position);
            gd.GetHeart();
        }

        Destroy(other.gameObject);
    }

    void SpawnEffect(GameObject effect, Vector3 pos)
    {
        if (effect != null)
            Instantiate(effect, pos, Quaternion.identity);
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }
}