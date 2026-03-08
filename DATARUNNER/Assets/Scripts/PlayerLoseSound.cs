using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip loseSound;

    void Start()
    {
        audioSource.PlayOneShot(loseSound);
    }
}