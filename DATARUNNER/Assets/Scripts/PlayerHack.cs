using UnityEngine;

public class PlayerHack : MonoBehaviour
{
    public EnemyHack enemy;

    void Update()
    {
        // Press H to hack enemy
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (enemy != null)
            {
                enemy.HackEnemy();
                Debug.Log("Hack Activated");
            }
            else
            {
                Debug.LogError("Enemy not assigned!");
            }
        }
    }
}