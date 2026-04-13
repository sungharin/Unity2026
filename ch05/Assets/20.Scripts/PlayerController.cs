using UnityEngine;

public class PlayerCintroller : MonoBehaviour
{
    public float Speed;  // 변수 선언 추가

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    //    void Update()
    //    {
    //        if (Input.GetKeyDown(KeyCode.LeftArrow))  
    //        {
    //            transform.Translate(-Speed, 0, 0);
    //        }

    //        if (Input.GetKeyDown(KeyCode.RightArrow))
    //        {
    //            transform.Translate(Speed, 0, 0);
    //        }
    //    }
    //}


    public void LButtonDown()
    {
        transform.Translate(-Speed, 0, 0);
    }
    public void RbButtonDown()
    {
        transform.Translate(Speed, 0, 0);
    }
}