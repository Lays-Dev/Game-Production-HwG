using UnityEngine;

public class EnemyCatchPlayer : MonoBehaviour
{
    public Transform player;
    public float catchDistance = 1.5f;

    private bool hasLost = false;

    void Update()
    {
        if (hasLost) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= catchDistance)
        {
            hasLost = true;
            GameManager.Instance.LoseGame();
        }
    }
}