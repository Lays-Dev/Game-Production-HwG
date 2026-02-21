// Pathway setup
// Create two empty game objects
// Point A
// Point B
// Place these at different parts of the map - The enemy will walk back and forth between these points

// Sound sphere setup
// Add a sphere collider on the enemy
// Trigger is true
// Radius adjusted to how far the enemy can hear
// Create a player tag
// Tag the player with player tag

// Enemy chase setup
// Enemy GameObject > 
//  > In the Nav-Mesh Agent increase Angular Speed to 360 
//        > Set stopping distance to 1


// EnemySound Script


using UnityEngine;
using UnityEngine.AI;

public class EnemySound : MonoBehaviour
{
    // Enemy delay when detecting the player
    [Header("Stun Settings")]
    // Delay time
    public float stunDuration = 1.2f; 
    private float stunTimer = 0f;
    private bool isStunned = false;
    private bool wasChasingLastFrame = false;

    // Enemy moves faster when chasing the player
    [Header("Movement Settings")]
    // Default speed
    public float patrolSpeed = 3.5f;
    // Chase speed
    public float chaseSpeed = 7.0f;
    // Reaches top speed over time
    public float chaseAcceleration = 12.0f; 
    // enum is being used as enemy memory to track what state the enemy is in
    public enum EnemyState { Patrolling, Investigating, Chasing }
    public EnemyState currentState = EnemyState.Patrolling;

    [Header("References")]
    public Transform player;
	// This is where the item is placed
    public Transform soundInvestigationPoint; 
    // Assign Point A and B here + other points
    public Transform[] patrolPoints;

    [Header("Settings")]
    public float viewDistance = 15f;
    public float fovAngle = 60f;
    
    private NavMeshAgent agent;
    private int patrolIndex;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        GoToNextPatrolPoint();
    }

    void Update() 
	// Checks if player is in sight before following the cautious path
	{
        bool canSee = CanSeePlayer();


        if (canSee) 
        {
            if (!wasChasingLastFrame)
            {
                isStunned = true;
                stunTimer = stunDuration;
                // Stop the enemy temporarily
                agent.isStopped = true; 
                // Trigger a shock animation + sound here
                Debug.Log("Enemy is shocked!");
            }

            wasChasingLastFrame = true;

            // Stun timer
            if (isStunned) 
            {
                // Slowly face the player while stunned
                Vector3 lookDir = player.position - transform.position;
                // Keep the enemy from tipping
                lookDir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0) 
                {
                    isStunned = false;
                    agent.isStopped = false;
                }
                return;
            }


            currentState = EnemyState.Chasing;
            // Chase functionality
            agent.speed = chaseSpeed;
            agent.acceleration = chaseAcceleration;
            // The enemy will go towards the player
            agent.SetDestination(player.position);
        } 

        else 
        {
            // Reset the "Spotted" flag when the player successfully hides
            wasChasingLastFrame = false;
            isStunned = false;

            if (currentState == EnemyState.Chasing) 
            {
                currentState = EnemyState.Patrolling;
                agent.speed = patrolSpeed;
                agent.isStopped = false;
                GoToNextPatrolPoint();
            }
            
            if (currentState == EnemyState.Patrolling) 
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f) GoToNextPatrolPoint();
            }
        }
    }

    // Triggered by the Sound Sphere (Sphere Collider)
    private void OnTriggerStay(Collider other) 
    {
        if (currentState != EnemyState.Chasing && other.CompareTag("Player")) 
        {
            // Check if player is sprinting or jumping (replace with input)
            bool isLoud = Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space);

            if (isLoud) {
                currentState = EnemyState.Investigating;
                agent.SetDestination(soundInvestigationPoint.position);
            }
        }
    }

    bool CanSeePlayer() {
        Vector3 dir = player.position - transform.position;
        if (Vector3.Angle(transform.forward, dir) < fovAngle * 0.5f) 
        {
            // Enemy wont hear the player if there is no direct line of sight
            if (Physics.Raycast(transform.position, dir.normalized, out RaycastHit hit, viewDistance)) 
            {
                return hit.transform == player;
            }
        }
        return false;
    }

    void GoToNextPatrolPoint() 
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[patrolIndex].position);
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    // This will tell the player 
    public void AlertToPlayerLocation()
    {
        currentState = EnemyState.Investigating;
        // This forces the enemy to go to the player, 
        agent.SetDestination(player.position); 
        agent.isStopped = false;
    }
}