using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame1()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGame2()
    {
        SceneManager.LoadScene("GameScene2");
    }
}