using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float dropSpeed = 0.1f;

    void Update()
    {
        transform.Translate(0, -dropSpeed, 0);  // ↓ 아래로 이동

        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }
}