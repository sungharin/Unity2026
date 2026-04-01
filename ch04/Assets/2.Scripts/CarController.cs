using UnityEngine;

public class CarController : MonoBehaviour
{
    float speed = 0f;
    Vector2 startPose;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;  
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPose = Input.mousePosition;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 endpos = Input.mousePosition;
            float swipeLen = endpos.x - startPose.x;
            speed =  swipeLen / 1000f;
            GetComponent<AudioSource>().Play();
        }
        transform.Translate(speed,0,0);
        speed *= 0.99f;
    }
}
