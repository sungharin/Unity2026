using UnityEngine;
using System.Collections;

public class BonusThrower : MonoBehaviour
{
    public GameObject bonusItemPrefab;
    public float throwInterval = 8f;
    public float throwTiming = 0.5f;

    Animator anim;
    float timer = 0f;
    bool isGameOver = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isGameOver) return;

        timer += Time.deltaTime;
        if (timer >= throwInterval)
        {
            timer = 0f;
            StartCoroutine(ThrowBonus());
        }
    }

    IEnumerator ThrowBonus()
    {
        if (anim != null)
            anim.Play("mixamo.com");

        yield return new WaitForSeconds(throwTiming);

        if (bonusItemPrefab == null) yield break;

        float x = Random.Range(-1, 2);
        float z = Random.Range(-1, 2);

        GameObject item = Instantiate(
            bonusItemPrefab,
            new Vector3(x, 7, z),
            Quaternion.identity
        );

        item.tag = "Heart";
        Debug.Log("하트 생성! 태그: " + item.tag);

        ItemController ic = item.GetComponent<ItemController>();
        if (ic != null)
        {
            ic.enabled = true;
            ic.SetSpeed(3f);
        }
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }
}