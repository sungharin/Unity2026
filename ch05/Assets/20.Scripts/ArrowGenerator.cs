using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    float span = 1.0f;
    float delta = 0f;
      

    // Update is called once per frame
    void Update()
    {
      delta += Time.deltaTime;
        if(delta > span)
        {
            GameObject go = Instantiate(arrowPrefab);
            float px = Random.Range(-8f, 4f);
            go.transform.position = new Vector3(px, 6f, 0);

            delta = 0;
        }
    }
}
