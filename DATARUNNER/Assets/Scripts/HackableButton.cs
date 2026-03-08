using UnityEngine;

public class HackableButton : MonoBehaviour
{
    public HackGroup hackGroup;

    [Header("VFX")]
    public GameObject currentVFX;
    public GameObject hackedVFXPrefab;

    private bool isHacked = false;

    public void Hack()
    {
        if (isHacked) return;
        isHacked = true;

        if (hackGroup != null)
        {
            hackGroup.RegisterHack();
        }

        if (currentVFX != null)
        {
            Destroy(currentVFX);
        }

        if (hackedVFXPrefab != null)
        {
            Instantiate(hackedVFXPrefab, transform.position, Quaternion.identity);
        }

        Debug.Log("Button hacked!");
    }
}