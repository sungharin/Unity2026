using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public Image hpGauge;

    public void DecreaseHP()
    {
        hpGauge.fillAmount -= 0.1f;
    }
}