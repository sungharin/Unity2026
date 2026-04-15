using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float jumpForce = 300f;
    float walkForce = 30f;
    float walkmaxspeed = 1f;

    public Sprite[] walkSprites;
    public float animationPeriod = 0.1f;
    float time = 0;
    int idx = 0;
    SpriteRenderer sr;

    Rigidbody2D rb;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector2.up * jumpForce);

            if (rb.linearVelocity.x < walkmaxspeed)
            {
                rb.AddForce(Vector2.right * walkForce);
            }
        }

        time += Time.deltaTime;

        if (time > animationPeriod)
        {
            time = 0;
            sr.sprite = walkSprites[idx];
            idx = 1 - idx;
        }
    }
}