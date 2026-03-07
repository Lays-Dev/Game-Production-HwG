using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu_UI;
    AudioSource buttonSound;

    void Start()
    {
        mainMenu_UI.SetActive(true);
        buttonSound = GetComponent<AudioSource>();
    }

    IEnumerator PlayAudioThenLoadScene(AudioSource buttonSFX, string sceneName)
    {
        buttonSFX.Play();
        yield return new WaitForSeconds(buttonSFX.clip.length);
        SceneManager.LoadScene(sceneName);
    }

    public void PlayButton()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "DATARUNNER2"));
    }

    public void CreditsButton()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "CreditsMenu"));
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

