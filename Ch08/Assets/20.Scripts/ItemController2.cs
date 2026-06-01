using UnityEngine;

public class ItemController2 : MonoBehaviour
{
    public float riseSpeed = 1f;
    public float maxHeight = 0.5f; // 최대 높이

    void Update()
    {
        transform.Translate(0, riseSpeed * Time.deltaTime, 0);

        // 살짝만 올라오고 사라짐
        if (transform.position.y > maxHeight)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        riseSpeed = speed;
    }
}