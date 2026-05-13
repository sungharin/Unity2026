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
        else if (Input.GetMouseButtonDown(0))
        {

        }
        

            float power = Input.mousePosition.y - startY;
            if (power < minPower) return;

            Vector3 dir = transform.forward + transform.up * 0.5f;
            bamsongi.GetComponent<Bamsongicontroller>().Shoot(dir * power * ThrowForce);

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //bamsongi.GetComponent<Bamsongicontroller>().Shoot(ray.direction * 2000);
        }
    }
}