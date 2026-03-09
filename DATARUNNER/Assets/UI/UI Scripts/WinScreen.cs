using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public GameObject firstSelectedButton;

public class WinScreen : MonoBehaviour
{
    public GameObject gameWin_UI;

    void Start()
    {
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("DATARUNNER2");
        AudioListener.pause = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioListener.pause = false;
    }
}
