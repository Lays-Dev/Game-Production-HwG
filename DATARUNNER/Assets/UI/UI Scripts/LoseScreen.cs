using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoseScreen : MonoBehaviour
{
    public GameObject gameLose_UI;
    public AudioSource loseSound;
    public GameObject firstSelectedButton;

    void Start()
    {
        loseSound.Play();

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
