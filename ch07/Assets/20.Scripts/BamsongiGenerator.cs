using UnityEngine;

public class BamsongiGenerator : MonoBehaviour

{
    public GameObject bamsongiPrefab;
    public float ThrowForce = 10f;

    float startY;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startY = Input.mousePosition.y;

        }
        else if (Input.GetMouseButtonDown(0))
        {
            GameObject bamsongi = Instantiate(bamsongiPrefab);
            bamsongi.transform.position = transform.position;

            float power = Input.mousePosition.y - startY;

            Vector3 dir = transform.forward + transform.up * 0.5f;
            bamsongi.GetComponent<Bamsongicontroller>().Shoot(dir * power * ThrowForce);

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //bamsongi.GetComponent<Bamsongicontroller>().Shoot(ray.direction * 2000);
        }
    }
}