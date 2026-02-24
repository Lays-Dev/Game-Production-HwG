using UnityEngine;
using UnityEngine.AI; // Required for Navigation

public class EnemyFollow : MonoBehaviour
{
    public Transform target; 
    // target point
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            // This tells the agent to find the path around the cube
            agent.SetDestination(target.position); 
        }
    }
}