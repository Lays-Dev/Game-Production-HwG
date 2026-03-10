using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinScreen : MonoBehaviour
{
    public GameObject gameWin_UI;
    public GameObject firstSelectedButton;
    AudioSource buttonSound;

    void Start()
    {
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
