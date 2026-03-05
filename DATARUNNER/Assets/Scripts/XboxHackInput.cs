using UnityEngine;

public class XboxHackInput : MonoBehaviour
{
    public EnemyHack enemy;

    void Update()
    {
        // Xbox X Button = Hack
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            if (enemy != null)
            {
                enemy.HackEnemy();
                Debug.Log("Xbox Hack Activated");
            }
        }
    }
}