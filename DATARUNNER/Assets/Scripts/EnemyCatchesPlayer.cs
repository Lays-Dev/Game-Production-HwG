using UnityEngine;

public class EnemyCatchPlayer : MonoBehaviour
{
    public Transform player;
    public float catchDistance = 1.5f;
    private EnemyHack enemyHack;
    private bool hasLost = false;

    void Start()
{
    enemyHack = GetComponent<EnemyHack>();
}

    void Update()
    {
        if (hasLost) return;
        if (enemyHack != null && enemyHack.IsHacked)
        {
            // Enemy is currently hacked, so it cannot catch the player
            return; 
        }
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= catchDistance)
        {
            hasLost = true;
            GameManager.Instance.LoseGame();
        }
    }
}