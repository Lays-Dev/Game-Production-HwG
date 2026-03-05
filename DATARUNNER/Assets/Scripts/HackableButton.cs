using UnityEngine;

public class HackableButton : MonoBehaviour
{
    public Door doorToOpen;

    public void Hack()
    {
        if (doorToOpen != null)
        {
            doorToOpen.OpenDoor();
            Debug.Log("Button Hacked!");
        }
    }
}
