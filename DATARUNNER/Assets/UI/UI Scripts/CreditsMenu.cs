using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public GameObject creditsMenu_UI;
    AudioSource buttonSound;

    void Start()
    {
        creditsMenu_UI.SetActive(true);
        buttonSound = GetComponent<AudioSource>();
    }

    IEnumerator PlayAudioThenLoadScene(AudioSource buttonSFX, string sceneName)
    {
        buttonSFX.Play();
        yield return new WaitForSeconds(buttonSFX.clip.length);
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnButton()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "MainMenu"));
    }
}
