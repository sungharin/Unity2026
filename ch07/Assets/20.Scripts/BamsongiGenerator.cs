using UnityEngine;

public class BamsongiGenerator : MonoBehaviour
{
    public GameObject bamsongiPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bamsongi = Instantiate(bamsongiPrefab);
            bamsongi.transform.position = transform.position;
            Vector3 dir = new Vector3(0, 200, 1000);
            bamsongi.GetComponent<Bamsongicontroller>().Shoot(dir);
        }
    }
}