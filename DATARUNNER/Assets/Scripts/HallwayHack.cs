using UnityEngine;

public class HallwayHack : MonoBehaviour
{
    public GameObject objectToDestroy1;
    public GameObject objectToDestroy2;

    public void Hack()
    {
        if (objectToDestroy1 != null)
        {
            Destroy(objectToDestroy1);
            Debug.Log("Object 1 hacked and destroyed!");
        }

        if (objectToDestroy2 != null)
        {
            Destroy(objectToDestroy2);
            Debug.Log("Object 2 hacked and destroyed!");
        }
    }
}