using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ControlsScene : MonoBehaviour
{
    public GameObject controlsMenu_UI;
    public GameObject firstSelectedButton;
    AudioSource buttonSound;

    void Start()
    {
        controlsMenu_UI.SetActive(true);
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

    public void ReturnButton()
    {
        StartCoroutine(PlayAudioThenLoadScene(buttonSound, "MainMenu"));
    }
}
