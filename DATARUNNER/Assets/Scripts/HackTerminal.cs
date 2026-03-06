using UnityEngine;

public class HackTerminal : MonoBehaviour
{
    public HallwayHack hallwayHack;
    private bool alreadyHacked = false;

    public void Hack()
    {
        if (alreadyHacked) return;

        alreadyHacked = true;

        if (hallwayHack != null)
        {
            hallwayHack.Hack();
        }

        Debug.Log(gameObject.name + " hacked");
    }
}