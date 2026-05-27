using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    Rigidbody rb;
    Animator anim;

    Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(xInput, 0, zInput);

        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();
            rb.MovePosition(rb.position +
                moveDirection * moveSpeed * Time.deltaTime);
            transform.forward = moveDirection;
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}