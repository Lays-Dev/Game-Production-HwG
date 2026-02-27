using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject loseScreenUI;

    void Awake()
    {
        Instance = this;
    }

    public void LoseGame()
    {
        loseScreenUI.SetActive(true);
        Time.timeScale = 0f; // Freeze game
    }
}