using UnityEngine;
using UnityEngine.UI;

public class sprintUI : MonoBehaviour
{
    public PlayerMovement player;
    public Image sprintImage;

    void Update()
    {
        if (player != null)
        {
            sprintImage.enabled = player.IsSprinting;
        }
    }
}