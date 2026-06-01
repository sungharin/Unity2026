using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [Header("UI Text")]
    public GameObject timeText;
    public GameObject pointText;
    public GameObject lifeText;
    public GameObject comboText;
    public GameObject levelText;

    [Header("UI Panel")]
    public GameObject gameOverPanel;
    public GameObject gradeText;

    [Header("UI Effect")]
    public Image redFlash;

    [Header("Game Settings")]
    public float totalTime = 60f;
    public int startLife = 3;

    float time;
    int point = 0;
    int life;
    int combo = 0;
    float comboTimer = 0f;
    int currentLevel = 1;
    bool isGameOver = false;
    bool isInvincible = false;

    TextMeshProUGUI timeTMP;
    TextMeshProUGUI pointTMP;
    TextMeshProUGUI lifeTMP;
    TextMeshProUGUI comboTMP;
    TextMeshProUGUI levelTMP;
    TextMeshProUGUI gradeTMP;

    Color[] skyColors = new Color[]
    {
        new Color(0.53f, 0.81f, 0.98f),
        new Color(0.99f, 0.72f, 0.35f),
        new Color(0.10f, 0.10f, 0.30f),
        new Color(0.05f, 0.05f, 0.05f),
    };

    void Start()
    {
        time = totalTime;
        life = startLife;

        timeTMP = timeText.GetComponent<TextMeshProUGUI>();
        pointTMP = pointText.GetComponent<TextMeshProUGUI>();
        lifeTMP = lifeText.GetComponent<TextMeshProUGUI>();
        comboTMP = comboText.GetComponent<TextMeshProUGUI>();
        levelTMP = levelText.GetComponent<TextMeshProUGUI>();

        if (gradeText != null)
            gradeTMP = gradeText.GetComponent<TextMeshProUGUI>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (redFlash != null)
            redFlash.color = new Color(1, 0, 0, 0);

        comboTMP.text = "";
        comboTMP.color = Color.white;

        Camera.main.backgroundColor = skyColors[0];
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
        levelTMP.text = "Lv." + currentLevel;

        if (combo > 0 && !isInvincible)
        {
            comboTimer -= Time.deltaTime;
            comboTMP.text = "Combo x" + combo + "!";
            comboTMP.color = GetComboColor(combo);
            if (comboTimer <= 0f)
                ResetCombo();
        }

        if (time <= 0)
        {
            time = 0;
            GameOver();
        }
    }

    public void GetApple()
    {
        combo++;
        comboTimer = 3f;
        int multiplier = Mathf.Min(combo, 5);
        int earned = 100 * multiplier;
        point += earned;

        if (combo >= 5)
        {
            GameObject basket = GameObject.Find("basket");
            if (basket != null)
                basket.GetComponent<BasketController>().StartAutoPilot(5f);
        }

        Debug.Log($"Apple! Combo x{combo} +{earned} Total:{point}");
    }

    public void GetBomb()
    {
        if (isInvincible) return;
        ResetCombo();
        life--;

        if (CameraShake.instance != null)
            CameraShake.instance.Shake(0.3f, 0.3f);

        StartCoroutine(RedFlashEffect());

        Debug.Log("Bomb! Life:" + life);

        if (life <= 0)
        {
            life = 0;
            GameOver();
            return;
        }
    }

    public void GetGoldenApple()
    {
        point += 500;
        time += 20f;
        StartCoroutine(SpecialScoreEffect());
        Debug.Log("Golden Apple! +500 +20sec");
    }

    public void GetStar()
    {
        StartCoroutine(StarEffect());
    }

    public void GetHeart()
    {
        if (life < startLife)
        {
            life++;
            StartCoroutine(HeartEffect());
        }
        Debug.Log("Heart! Life:" + life);
    }

    public void MissApple()
    {
        ResetCombo();
        Debug.Log("Missed! Combo Reset");
    }

    public void OnLevelUp(int level)
    {
        currentLevel = level;
        int colorIndex = Mathf.Min(level - 1, skyColors.Length - 1);
        StartCoroutine(ChangeSkyColor(skyColors[colorIndex]));
        Debug.Log("Level Up! Lv." + level);
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

    IEnumerator ChangeSkyColor(Color targetColor)
    {
        Color startColor = Camera.main.backgroundColor;
        float elapsed = 0f;
        float duration = 2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Camera.main.backgroundColor = Color.Lerp(startColor, targetColor, elapsed / duration);
            yield return null;
        }

        Camera.main.backgroundColor = targetColor;
    }

    IEnumerator HeartEffect()
    {
        comboTMP.color = Color.red;
        comboTMP.text = "EXTRA LIFE!";
        yield return new WaitForSeconds(2f);
        comboTMP.text = "";
        comboTMP.color = Color.white;
    }

    IEnumerator SpecialScoreEffect()
    {
        comboTMP.color = Color.yellow;
        comboTMP.text = "SPECIAL SCORE!!";
        yield return new WaitForSeconds(2f);
        comboTMP.text = "";
        comboTMP.color = Color.white;
    }

    IEnumerator StarEffect()
    {
        isInvincible = true;

        float timer = 5f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            comboTMP.color = Color.cyan;
            comboTMP.text = "INVINCIBLE! " + timer.ToString("F1");
            yield return null;
        }

        isInvincible = false;
        comboTMP.text = "";
        comboTMP.color = Color.white;
    }

    Color GetComboColor(int combo)
    {
        if (combo >= 5) return new Color(1f, 0.84f, 0f);
        if (combo >= 4) return Color.magenta;
        if (combo >= 3) return Color.blue;
        if (combo >= 2) return Color.green;
        return Color.white;
    }

    void ResetCombo()
    {
        combo = 0;
        comboTimer = 0f;
        if (!isInvincible)
        {
            comboTMP.text = "";
            comboTMP.color = Color.white;
        }
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        time = 0;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gradeTMP != null)
        {
            string grade = GetGrade(point);
            gradeTMP.text = "Grade: " + grade;
            gradeTMP.color = GetGradeColor(grade);
        }

        GameObject basket = GameObject.Find("basket");
        if (basket != null)
            basket.GetComponent<BasketController>().OnGameOver();

        GameObject generator = GameObject.Find("ItemGenerator");
        if (generator != null)
            generator.GetComponent<ItemGenerator>().OnGameOver();

        Debug.Log("Game Over! Final Score: " + point);
    }

    string GetGrade(int score)
    {
        if (score >= 3000) return "S";
        if (score >= 2000) return "A";
        if (score >= 1000) return "B";
        if (score >= 500) return "C";
        return "F";
    }

    Color GetGradeColor(string grade)
    {
        if (grade == "S") return new Color(1f, 0.84f, 0f);
        if (grade == "A") return Color.red;
        if (grade == "B") return Color.green;
        if (grade == "C") return Color.cyan;
        return Color.grey;
    }

    string GetHearts(int count)
    {
        string hearts = "";
        for (int i = 0; i < count; i++) hearts += "<color=red>♥</color> ";
        for (int i = count; i < startLife; i++) hearts += "<color=grey>♥</color> ";
        return hearts;
    }

    public int GetPoint() => point;
}