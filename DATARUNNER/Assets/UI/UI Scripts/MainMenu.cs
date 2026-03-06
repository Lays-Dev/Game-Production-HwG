using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu_UI;

    void Start()
    {
        mainMenu_UI.SetActive(true);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("DATARUNNER2");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

