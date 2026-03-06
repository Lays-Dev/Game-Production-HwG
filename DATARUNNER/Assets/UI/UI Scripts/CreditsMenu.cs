using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public GameObject creditsMenu_UI;

    void Start()
    {
        creditsMenu_UI.SetActive(true);
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}