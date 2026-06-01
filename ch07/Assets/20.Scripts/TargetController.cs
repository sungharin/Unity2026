using UnityEngine;

public class TargetController : MonoBehaviour
{
    GameObject player;
    TargetGenerate tg;

    private void Start()
    {
        player = GameObject.Find("Player");
        tg = FindObjectOfType<TargetGenerate>();
    }

    private void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bamsongi"))
        {
            if (tg != null)
            {
                tg.GenerateTarget();
            }

            Destroy(gameObject);
        }
    }
}