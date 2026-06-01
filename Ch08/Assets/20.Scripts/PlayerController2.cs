using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip appleSE;
    public AudioClip bombSE;
    public AudioClip heartSE;

    GameObject director;
    AudioSource aud;
    bool isGameOver = false;

    void Start()
    {
        director = GameObject.Find("GameDirector2");
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isGameOver) return;

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

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        Debug.Log("충돌! 태그: " + other.gameObject.tag);

        GameDirector2 gd = director.GetComponent<GameDirector2>();

        if (other.gameObject.tag == "Apple")
        {
            if (appleSE != null) aud.PlayOneShot(appleSE);
            gd.GetApple();
        }
        else if (other.gameObject.tag == "Heart")
        {
            if (heartSE != null) aud.PlayOneShot(heartSE);
            gd.GetHeart();
        }
        else if (other.gameObject.tag == "Bomb")
        {
            if (bombSE != null) aud.PlayOneShot(bombSE);
            gd.GetBomb();
        }

        Destroy(other.gameObject);
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }
}