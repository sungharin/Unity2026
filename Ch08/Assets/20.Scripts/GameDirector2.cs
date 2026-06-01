using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameDirector2 : MonoBehaviour
{
    [Header("UI Text")]
    public GameObject timeText;
    public GameObject pointText;
    public GameObject lifeText;
    public GameObject comboText;

    [Header("UI Panel")]
    public GameObject gameOverPanel;

    [Header("UI Effect")]
    public Image redFlash;

    [Header("Game Settings")]
    public float totalTime = 60f;
    public int startLife = 3;

    float time;
    int point = 0;
    int life;
    bool isGameOver = false;

    TextMeshProUGUI timeTMP;
    TextMeshProUGUI pointTMP;
    TextMeshProUGUI lifeTMP;
    TextMeshProUGUI comboTMP;

    void Start()
    {
        time = totalTime;
        life = startLife;

        timeTMP = timeText.GetComponent<TextMeshProUGUI>();
        pointTMP = pointText.GetComponent<TextMeshProUGUI>();
        lifeTMP = lifeText.GetComponent<TextMeshProUGUI>();
        comboTMP = comboText.GetComponent<TextMeshProUGUI>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (redFlash != null)
            redFlash.color = new Color(1, 0, 0, 0);

        comboTMP.text = "";
    }

    void Update()
    {
        if (isGameOver) return;

        time -= Time.deltaTime;

        if (time <= 10f && time > 0)
            timeTMP.color = Mathf.Sin(Time.time * 5f) > 0 ? Color.red : Color.white;
        else
            timeTMP.color = Color.white;

        timeTMP.text = "Time: " + Mathf.Max(0, time).ToString("F1");
        pointTMP.text = "Point: " + point;
        lifeTMP.text = "Life: " + GetHearts(life);

        if (time <= 0)
        {
            time = 0;
            GameOver();
        }
    }

    public void GetApple()
    {
        point += 100;
        StartCoroutine(ShowMessage("APPLE! +100", Color.green));
        Debug.Log("Apple! " + point);
    }

    public void GetHeart()
    {
        if (life < startLife)
        {
            life++;
            StartCoroutine(ShowMessage("EXTRA LIFE!", Color.red));
        }
        Debug.Log("Heart! Life:" + life);
    }

    public void GetBomb()
    {
        life--;
        StartCoroutine(RedFlashEffect());
        Debug.Log("Bomb! Life:" + life);

        if (life <= 0)
        {
            life = 0;
            GameOver();
        }
    }

    IEnumerator ShowMessage(string msg, Color color)
    {
        comboTMP.color = color;
        comboTMP.text = msg;
        yield return new WaitForSeconds(2f);
        comboTMP.text = "";
        comboTMP.color = Color.white;
    }

    IEnumerator RedFlashEffect()
    {
        if (redFlash == null) yield break;
        redFlash.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.1f);
        redFlash.color = new Color(1, 0, 0, 0.3f);
        yield return new WaitForSeconds(0.1f);
        redFlash.color = new Color(1, 0, 0, 0);
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        time = 0;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        GameObject character = GameObject.Find("Character");
        if (character != null)
            character.GetComponent<PlayerController2>().OnGameOver();

        GameObject generator = GameObject.Find("ItemGenerator2");
        if (generator != null)
            generator.GetComponent<ItemGenerator2>().OnGameOver();

        Debug.Log("Game Over! Final Score: " + point);
    }

    string GetHearts(int count)
    {
        string hearts = "";
        for (int i = 0; i < count; i++) hearts += "<color=red>♥</color> ";
        for (int i = count; i < startLife; i++) hearts += "<color=grey>♥</color> ";
        return hearts;
    }
}