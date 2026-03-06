using UnityEngine;

public class HackManager : MonoBehaviour
{
    public GameObject objectToDestroy;

    private int hackCount = 0;
    public int hacksRequired = 2;

    public void Hack()
    {
        hackCount++;

        Debug.Log("Hack activated: " + hackCount + "/" + hacksRequired);

        if (hackCount >= hacksRequired)
        {
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
                Debug.Log("Both hacks complete. Object destroyed!");
            }
        }
    }
}