using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float loseSightTime = 2f;   // Memory time
    public float pauseTime = 0.5f;

    private NavMeshAgent agent;
    private float loseTimer;
    private bool isPaused = false;
    private float pauseTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            loseTimer = loseSightTime;

            if (!isPaused)
            {
                isPaused = true;
                pauseTimer = pauseTime;
                agent.isStopped = true;
            }
        }

        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;

            if (pauseTimer <= 0)
            {
                isPaused = false;
                agent.isStopped = false;
            }

            return;
        }

        if (loseTimer > 0)
        {
            loseTimer -= Time.deltaTime;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }
}