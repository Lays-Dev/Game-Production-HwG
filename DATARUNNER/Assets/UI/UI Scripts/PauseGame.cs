using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu_UI;

    void Start()
    {
        pauseMenu_UI.SetActive(false);
    }

    void Update()
    {
        // Pauses Game when Escape Key is Pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu_UI.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
    }

    // Hides the Pause Menu and Resumes the Game
    public void ResumeButton()
    {
        pauseMenu_UI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu_UI");
        AudioListener.pause = false;
    }
}