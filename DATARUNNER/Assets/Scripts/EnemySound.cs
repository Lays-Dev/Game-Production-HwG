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
    private float memoryTimer = 0f;
    public float memoryDuration = 2f;

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

    [Header("GameObjects")]
    public Transform player;
	// This is where the item is placed
    public Transform ItemPedistal; 
    // Assign Point A and B here + other points
    public Transform[] patrolPoints;

    [Header("Settings")]
    public float viewDistance = 15f;
    public float fovAngle = 60f;
    private PlayerMovement playerMovement;
    private NavMeshAgent agent;
    private int patrolIndex;
    private float currentSpeed;
private Animator animator;

// Keeps enemy from getting stuck
    [Header("Stuck Settings")]
    public float stuckTimeThreshold = 3f;
    public float stuckVelocityThreshold = 0.1f;

    private float stuckTimer = 0f;

    // Hack variables
    private EnemyHack enemyHack;

    [Header("Enemy Alert UI")]
    public EnemyAlertUI alertUI;

    [Header("Enemy Audio")]
    public AudioSource enemyDetectsSound;

    void Start() {
        playerMovement = player.GetComponent<PlayerMovement>();
         animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyHack = GetComponent<EnemyHack>();
        GoToNextPatrolPoint();
    }

    void Update() 
	// Checks if player is in sight before following the cautious path
	{
        CheckIfStuck();

    float currentSpeed = agent.velocity.magnitude;
    animator.SetFloat("Speed", currentSpeed);

        if (playerMovement != null && playerMovement.IsSprinting)
        {
            Debug.Log("Enemy detects sprinting");
        }
        // Memory logic
        bool canSee = CanSeePlayer();
        // Change UI icon
        if (alertUI != null)
        {
            alertUI.SetDetected(currentState == EnemyState.Chasing);
        }
        // Enemy can hear the player if they are sprinting, unless the enemy is hacked
        bool canHearSprint = playerMovement != null && playerMovement.IsSprinting && (enemyHack == null || !enemyHack.IsHacked);

        if (canSee || canHearSprint)
        {
            memoryTimer = memoryDuration;
        }
        else
        {
            memoryTimer -= Time.deltaTime;
        }

        // Set currentSpeed based on agent's velocity
        currentSpeed = agent.velocity.magnitude;

        // Update Animator parameter
        animator.SetFloat("Speed", currentSpeed);


        if (memoryTimer > 0)
        {
            if (!wasChasingLastFrame && !isStunned)
            {
                isStunned = true;
                stunTimer = stunDuration;
                // Stop the enemy temporarily
                agent.isStopped = true; 
                // Trigger a shock animation + sound here
                Debug.Log("Enemy is shocked!");
                enemyDetectsSound.Play();
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
                    // Enemy resumes after stun
                    agent.ResetPath();
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
            //isStunned = false;
            // This line was causing the enemy to stop every frame

            if (currentState == EnemyState.Chasing) 
            {
                currentState = EnemyState.Patrolling;
                agent.speed = patrolSpeed;
                enemyDetectsSound.Stop();
                agent.isStopped = false;
                GoToNextPatrolPoint();
            }
            
            if (currentState == EnemyState.Patrolling) 
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f) GoToNextPatrolPoint();
            }
        }
    }
    //

    void CheckIfStuck()
    {
        if (!agent.pathPending &&
            agent.hasPath &&
            agent.remainingDistance > agent.stoppingDistance &&
            agent.velocity.magnitude < stuckVelocityThreshold)
        {
            stuckTimer += Time.deltaTime;

            if (stuckTimer >= stuckTimeThreshold)
            {
                Debug.Log("Enemy was stuck! Recalculating path...");

                Vector3 currentDestination = agent.destination;

                agent.ResetPath();
                agent.SetDestination(currentDestination);

                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }
    }

    // Triggered by the Sound Sphere (Sphere Collider)
    private void OnTriggerStay(Collider other) 
    {
        if (currentState != EnemyState.Chasing && other.CompareTag("Player")) 
        {
            // Check if player is sprinting or jumping (replace with input) UNLESS hacked
            bool isLoud = playerMovement != null && playerMovement.IsSprinting && (enemyHack == null || !enemyHack.IsHacked);
            if (isLoud)
            {
                //memoryTimer = memoryDuration;
                currentState = EnemyState.Chasing;
                agent.speed = chaseSpeed;
                agent.acceleration = chaseAcceleration;
                agent.SetDestination(player.position);
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

    // Chase player when data chip is picked up
    public void OnDataChipStolen()
    {
        Debug.Log("Data Chip stolen! Enemy is now hunting player!");

        // Clear any memory timers
        memoryTimer = memoryDuration;

        // Cancel stun if active
        isStunned = false;
        agent.isStopped = false;

        // Force chase state
        currentState = EnemyState.Chasing;

        agent.speed = chaseSpeed;
        agent.acceleration = chaseAcceleration;

        agent.SetDestination(player.position);
    }
}
