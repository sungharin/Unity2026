using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    public GameObject timeText;
    public GameObject pointText;

    float time = 60.0f;
    int point = 0;

    // Update is called once per frame
    void Update()
    {
        if (time < 0) return;


        time -= Time.deltaTime;
        timeText.GetComponent<TextMeshProUGUI>().text =
            "Time: " + time.ToString("F1");


        timeText.GetComponent<TextMeshProUGUI>().text =
          "Point: " + point;
    }

    public void GetApple()
    {
        point += 100;
    }

    public void GetBomb()
    {
        point /= 2;
    }
   
    }

