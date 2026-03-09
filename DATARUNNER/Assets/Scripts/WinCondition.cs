using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static bool objectDestroyed = false;

    void Start()
    {
        objectDestroyed = false; // reset when level loads
    }

    void OnDestroy()
    {
        objectDestroyed = true;
        Debug.Log("Objective object destroyed!");
    }
}