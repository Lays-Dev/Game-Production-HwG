using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinScreen : MonoBehaviour
{
    public GameObject gameWin_UI;
    public GameObject firstSelectedButton;

    void Start()
    {
            EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(firstSelectedButton);
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
