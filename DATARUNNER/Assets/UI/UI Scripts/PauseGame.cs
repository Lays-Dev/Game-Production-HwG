using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu_UI;
    public static bool isPaused = false;
    public GameObject firstButton;
    public GameObject firstSelectedButton;

    void Start()
    {
        pauseMenu_UI.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
{
    bool pausePressed = Input.GetKeyDown(KeyCode.Escape);

    if (Gamepad.current != null)
    {
        pausePressed |= Gamepad.current.startButton.wasPressedThisFrame;
    }

    if (pausePressed)
    {
        if (isPaused)
            ResumeButton();
        else
            Pause();
    }
}

void Pause()
{
    pauseMenu_UI.SetActive(true);
    Time.timeScale = 0f;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    AudioListener.pause = true;
    isPaused = true;

    EventSystem.current.SetSelectedGameObject(firstSelectedButton);
}
    // Hides the Pause Menu and Resumes the Game
    public void ResumeButton()
    {
         Debug.Log("Resume clicked");
        pauseMenu_UI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
         Debug.Log("Main menu clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        AudioListener.pause = false;
    }
}