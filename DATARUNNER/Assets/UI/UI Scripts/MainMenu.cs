using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu_UI;
    public GameObject firstSelectedButton;
    AudioSource buttonSound;

    void Start()
    {
        mainMenu_UI.SetActive(true);
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

    IEnumerator FadeOutAndDestroy()
    {
        AudioSource music = MenuMusic.instance.GetComponent<AudioSource>();

        while (music.volume > 0)
        {
            music.volume -= Time.deltaTime;
            yield return null;
        }

        Destroy(MenuMusic.instance.gameObject);
    }

    public void PlayButton()
    {
        CursorReset();
        StartCoroutine(FadeOutAndDestroy());
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "DATARUNNER2"));
    }

    public void CreditsButton()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "CreditsMenu"));
    }

    public void ControlsButton()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "ControlsScreen"));
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    void CursorReset()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

