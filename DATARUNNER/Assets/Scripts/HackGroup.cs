using UnityEngine;

public class HackGroup : MonoBehaviour
{
    private int hacksCompleted = 0;

    [Header("Destroy After 1 Hack")]
    public GameObject object1A;
    public GameObject object1B;

    [Header("Destroy After 2 Hacks")]
    public GameObject object2A;
    public GameObject object2B;

    [Header("Destroy After 4 Hacks")]
    public GameObject finalObject;

    public void RegisterHack()
    {
        hacksCompleted++;

        Debug.Log("Hack progress: " + hacksCompleted);

        if (hacksCompleted == 1)
        {
            DestroyObject(object1A);
            DestroyObject(object1B);
        }

        if (hacksCompleted == 2)
        {
            DestroyObject(object2A);
            DestroyObject(object2B);
        }

        if (hacksCompleted == 4)
        {
            DestroyObject(finalObject);
        }
    }

    void DestroyObject(GameObject obj)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}