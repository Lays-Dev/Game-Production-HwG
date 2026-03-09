using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CreditsMenu : MonoBehaviour
{
    public GameObject creditsMenu_UI;
    public GameObject firstSelectedButton;
    AudioSource buttonSound;

    void Start()
    {
        creditsMenu_UI.SetActive(true);
        buttonSound = GetComponent<AudioSource>();
    }

    IEnumerator PlayAudioThenLoadScene(AudioSource buttonSFX, string sceneName)
    {
        buttonSFX.Play();
        yield return new WaitForSeconds(buttonSFX.clip.length - 0.25f);
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnButton()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "MainMenu"));
    }
}
