using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static bool objectDestroyed = false;

    void OnDestroy()
    {
        objectDestroyed = true;
        Debug.Log("Objective object destroyed!");
    }
}