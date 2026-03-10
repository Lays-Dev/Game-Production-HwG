using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCatchPlayer : MonoBehaviour
{
    public Transform player;
    public float catchDistance = 1.5f;

    public LayerMask sightLayers; // player + environment layers

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
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= catchDistance && CanSeePlayer())
        {
            hasLost = true;

            Time.timeScale = 1f;
            AudioListener.pause = false;

            SceneManager.LoadScene("GameOverScreen");
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, catchDistance, sightLayers))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }
}