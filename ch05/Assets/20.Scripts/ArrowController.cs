using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject player;
    GameObject director;

    float minDistance = 1.1f;

    public float dropSpeed = 0.1f;

    private void Start()
    {
        player = GameObject.Find("Player");
        director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        transform.Translate(0, -dropSpeed, 0);

        if (transform.position.y < -7)
        {
            director.GetComponent<GameDirector>().DecreaseHP();
            Destroy(gameObject);
        }

        Vector2 p1 = transform.position;
        Vector2 p2 = player.transform.position;
        float distance = (p1 - p2).magnitude;

        if (distance < minDistance)
        {
            director.GetComponent<GameDirector>().DecreaseHP();
            Destroy(gameObject);
        }
    }
}