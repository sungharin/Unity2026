using UnityEngine;

public class PlayerMoveRigidbody : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;

    Rigidbody rb;
      
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        
        }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * rotateSpeed * Time.deltaTime;
        float zSpeed = zInput * moveSpeed * Time.deltaTime;

       // transform.Translate(0, 0, zSpeed);
        transform.Rotate(0, xSpeed, 0);
        rb.linearVelocity = zSpeed * transform.forward;
    }
}
