using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public string winSceneName = "WinScreen";

    void OnTriggerEnter(Collider other)
    {
        if (!WinCondition.objectDestroyed) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player escaped!");

            Time.timeScale = 1f;
            SceneManager.LoadScene(winSceneName);
        }
    }
}