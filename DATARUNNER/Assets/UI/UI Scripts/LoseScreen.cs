using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoseScreen : MonoBehaviour
{
    public GameObject gameLose_UI;
    public AudioSource loseSound;
    AudioSource buttonSound;
    public GameObject firstSelectedButton;

    void Start()
    {
        loseSound.Play();

        buttonSound = GetComponent<AudioSource>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    IEnumerator PlayAudioThenLoadScene(AudioSource buttonSFX, string sceneName)
    {
        buttonSFX.Play();
        yield return new WaitForSeconds(buttonSFX.clip.length - 0.25f);
        SceneManager.LoadScene(sceneName);
    }

    public void RetryButton()
    {
        CursorReset();
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "DATARUNNER2"));
        AudioListener.pause = false;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "MainMenu"));
        AudioListener.pause = false;
    }

    void CursorReset()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
