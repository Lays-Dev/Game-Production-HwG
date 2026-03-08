using UnityEngine;

public class GameWinSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip winSound;

    void Start()
    {
        audioSource.PlayOneShot(winSound);
    }
}