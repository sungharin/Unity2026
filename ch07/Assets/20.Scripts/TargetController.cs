using UnityEngine;

public class TargetController : MonoBehaviour
{

    GameObject player;

    private void Start()
    {
        player = GameObject.Find("player");
    }
    private void Update()
    {
        transform.LookAt(player.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bamsongi"))
        {
            Destroy(gameObject);
        }
    }
}
