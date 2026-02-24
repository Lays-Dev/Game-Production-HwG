// Create two empty game objects
// Point A
// Point B
// Place these at different parts of the map - The enemy will walk back and forth between these points
// Pathway Script

using UnityEngine;
using UnityEngine.AI;

public class Pathway : MonoBehaviour
{
	// Assign Point A and B here
    public Transform[] waypoints; 
    public Transform player;
    public float viewDistance = 10f;
    public float fieldOfViewAngle = 45f;
    
    private NavMeshAgent agent;
    private int currentTargetIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextWaypoint();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            // Stop immediately if player is in sight
            agent.isStopped = true; 
        }
        else
        {
            // Resume pathway movement if player is not in sight
            agent.isStopped = false;

            // Check if we reached the current waypoint to switch to the next
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextWaypoint();
            }
        }
    }

    void GoToNextWaypoint()
    // Cycles through the waypoints in order, looping back to the first after reaching the last
    // Array of waypoints is assigned in the inspector, and the enemy will move to each one in sequence
    {
        if (waypoints.Length == 0) return;
        agent.SetDestination(waypoints[currentTargetIndex].position);
        currentTargetIndex = (currentTargetIndex + 1) % waypoints.Length;
    }

    bool CanSeePlayer()
    {
        // Vector3.Angle is the vision cone
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // Player enters the vision cone check
        if (angle < fieldOfViewAngle * 0.5f)
        {
            // Player distance check
            if (directionToPlayer.magnitude < viewDistance)
            {
                // Physics.Raycast is the line of sight check, it checks if there are any obstacles between the enemy and the player
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, viewDistance))
                {
                    if (hit.transform == player) return true;
                }
            }
        }
        return false;
    }
}