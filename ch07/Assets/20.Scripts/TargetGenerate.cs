using UnityEngine;

public class TargetGenerate : MonoBehaviour
{
    public GameObject targetPrefab;
    public float minDistance = 10f;

    Transform[] targetPosition;

    void Start()
    {
        targetPosition = GetComponentsInChildren<Transform>();
    }

    public void GenerateTarget()
    {
        int index;
        do
        {
            index = Random.Range(1, targetPosition.Length);
        } while (Vector3.Distance(transform.position, targetPosition[index].position)
            < minDistance);

        Vector3 position = targetPosition[index].position;

        GameObject target = Instantiate(targetPrefab,
            position,
            Quaternion.identity);
        target.transform.SetParent(transform);
    }
}