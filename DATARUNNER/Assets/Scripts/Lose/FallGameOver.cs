using UnityEngine;
using UnityEngine.SceneManagement;

public class FallGameOver : MonoBehaviour
{
    public string gameOverScene = "GameOverScreen";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(gameOverScene);
        }
    }
}