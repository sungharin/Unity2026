using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float dropSpeed = 1f;
    public float rotateSpeed = 180f;
    bool isReversed = false;

    void Update()
    {
        if (isReversed)
            transform.Translate(0, dropSpeed * Time.deltaTime, 0);
        else
            transform.Translate(0, -dropSpeed * Time.deltaTime, 0);

        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        if (transform.position.y < -5f || transform.position.y > 15f)
        {
            NotifyMiss();
            Destroy(gameObject);
        }
    }

    void NotifyMiss()
    {
        if (gameObject.tag == "Apple" || gameObject.tag == "GoldenApple")
        {
            GameObject director = GameObject.Find("GameDirector");
            if (director != null)
                director.GetComponent<GameDirector>().MissApple();
        }
    }

    public void SetSpeed(float speed)
    {
        dropSpeed = speed;
    }

    public void SetReversed(bool reversed)
    {
        isReversed = reversed;
    }
}