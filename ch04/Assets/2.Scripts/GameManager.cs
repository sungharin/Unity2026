using TMPro;
using UnityEngine;
// using static UnityEngine.RuleTile.TilingRuleOutput;  ← 삭제

public class GameManager : MonoBehaviour
{
    //public GameObject car;
    //public GameObject flag;
    //public GameObject distance;
    public Transform car;
    public Transform flag;
    public TextMeshProUGUI distance;

    void Start()
    {
        car = GameObject.Find("Car").transform;
    }

    void Update()
    {
        float length = flag.position.x
            - car.position.x;

        distance.text =
            "Distance : " + length.ToString("F2") + "m";
    }

}