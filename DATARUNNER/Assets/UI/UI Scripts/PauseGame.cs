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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AudioListener.pause = true;
        }
    }

    // Hides the Pause Menu and Resumes the Game
    public void ResumeButton()
    {
        pauseMenu_UI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioListener.pause = false;
    }
}