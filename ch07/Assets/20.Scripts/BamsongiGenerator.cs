using UnityEngine;

public class BamsongiGenerator : MonoBehaviour
{
    public GameObject bamsongiPrefab;
    public float ThrowForce = 10f;
    public float minPower = 10f;

    float startY;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))  // Down → Up 으로 수정
        {
            float power = Input.mousePosition.y - startY;
            if (power < minPower) return;

            GameObject bamsongi = Instantiate(bamsongiPrefab, transform.position, transform.rotation);
            Vector3 dir = transform.forward + transform.up * 0.5f;
            bamsongi.GetComponent<Bamsongicontroller>().Shoot(dir * power * ThrowForce);
        }
    }
}