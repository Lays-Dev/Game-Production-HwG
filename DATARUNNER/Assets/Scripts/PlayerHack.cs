using UnityEngine;

public class PlayerHack : MonoBehaviour
{
    public EnemyHack enemy;

    [Header("Hack Timing")]
    public float firstUnlockDelay = 15f;
    public float hackCooldown = 60f;

    private float unlockTimer = 0f;
    private float cooldownTimer = 0f;

    private bool hackUnlocked = false;
    private bool hackReady = false;

    void Update()
    {
        // First unlock countdown
        if (!hackUnlocked)
        {
            unlockTimer += Time.deltaTime;

            if (unlockTimer >= firstUnlockDelay)
            {
                hackUnlocked = true;
                hackReady = true;
                Debug.Log("Hack ability unlocked!");
            }

            return;
        }

        // Cooldown countdown
        if (!hackReady)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= hackCooldown)
            {
                hackReady = true;
                cooldownTimer = 0f;
                Debug.Log("Hack is ready again!");
            }
        }

        // Left click or Q to hack
        if (hackReady && (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0)))
        {
            if (enemy != null)
            {
                enemy.HackEnemy();
                Debug.Log("Hack Activated");

                hackReady = false; // start cooldown
            }
            else
            {
                Debug.LogError("Enemy not assigned!");
            }
        }
    }
}