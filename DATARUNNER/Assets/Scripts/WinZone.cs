using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public string winSceneName = "WinScreen";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && WinCondition.objectDestroyed)
        {
            Debug.Log("Player escaped!");

            Time.timeScale = 1f; // make sure time isn't paused
            SceneManager.LoadScene(winSceneName);
        }
    }
}