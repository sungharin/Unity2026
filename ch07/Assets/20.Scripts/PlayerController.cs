using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 1f;

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * rotateSpeed * Time.deltaTime;
        float zSpeed = zInput * moveSpeed * Time.deltaTime;

        transform.Translate(0, 0, zSpeed);  
        transform.Rotate(0, xSpeed, 0);
    }
}