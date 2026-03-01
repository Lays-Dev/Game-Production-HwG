using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHack : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool canSeePlayer = true;
    public bool canHearPlayer = true;

    private bool isHacked = false;
    public bool IsHacked => isHacked;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent missing on Enemy!");
        }
    }

    public void HackEnemy()
    {
        if (!isHacked)
        {
            StartCoroutine(HackRoutine());
        }
    }

    IEnumerator HackRoutine()
    {
        isHacked = true;

        Debug.Log("Enemy Hacked");

        // Stop enemy movement safely
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
        }

        // Disable detection
        canSeePlayer = false;
        canHearPlayer = false;

        // Wait 5 seconds
        yield return new WaitForSeconds(5f);

        // Restore enemy
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = false;
        }

        canSeePlayer = true;
        canHearPlayer = true;

        Debug.Log("Enemy Restored");

        isHacked = false;
    }
}