using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotateSpeed = 20.0f;
    public float shootingForce = 100f;

    public GameObject bamsongiPrefab;
    public Transform shootingPoint;

    Rigidbody rb;
    Animator anim;

    Vector3 moveDirection;

    float xInput;
    float zInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Fire");
            Shooting();
            return;
        }


        moveDirection = new Vector3(xInput, 0, zInput);

        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();
            anim.SetBool("IsWalking", true);

            Vector3 move = new Vector3(0, 0, zInput);

            //transform.forward = moveDirection;  // Rotation

            Rotate();

            rb.MovePosition(rb.position +
                move * moveSpeed * Time.deltaTime);

        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    void Rotate()
    {
        float rotSpeed = xInput * rotateSpeed * Time.deltaTime;
        rb.rotation = Quaternion.Euler(0, rotSpeed, 0) *
            rb.rotation;
    }

    void Shooting()
    {
        GameObject bamsongi = Instantiate(bamsongiPrefab,
            shootingPoint.position,
            shootingPoint.rotation);
        Vector3 dir = shootingPoint.forward * shootingForce;
        bamsongi.GetComponent<Bamsongicontroller>().Shoot(dir);
    }
}
