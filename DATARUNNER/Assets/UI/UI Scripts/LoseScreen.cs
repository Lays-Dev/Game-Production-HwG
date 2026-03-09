using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public GameObject firstSelectedButton;

public class LoseScreen : MonoBehaviour
{
    public GameObject gameLose_UI;
    public AudioSource loseSound;

    void Start()
    {
        loseSound.Play();
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
