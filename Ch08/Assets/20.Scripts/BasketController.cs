using UnityEngine;

public class BasketController : MonoBehaviour
{
    public AudioClip appleSE;
    public AudioClip bombSE;

    GameObject director;

    AudioSource aud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        aud = GetComponent<AudioSource>();
        director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
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
        if (other.gameObject.tag == "Apple")
        {
            Debug.Log("사과를 잡았다.");
            aud.PlayOneShot(appleSE);
            director.GetComponent<GameDirector>().GetApple();

        }
        else if (other.gameObject.tag == "Bomb")
        {
            Debug.Log("폭탄을 잡았다.");
            aud.PlayOneShot(bombSE);
            director.GetComponent<GameDirector>().GetBomb();

        }

        Destroy(other.gameObject);
    }
}
