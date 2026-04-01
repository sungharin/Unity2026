using UnityEngine;

public class PlayerCintroller : MonoBehaviour
{
    public float Speed;  // 변수 선언 추가

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))  // ; 제거
        {
            transform.Translate(-Speed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))  // 오타 수정 + ; 제거
        {
            transform.Translate(Speed, 0, 0);
        }
    }
}
