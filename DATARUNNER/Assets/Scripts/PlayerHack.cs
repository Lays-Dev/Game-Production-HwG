using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI")]
    public Image hackIcon;

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

                if (hackIcon != null)
                    hackIcon.enabled = true;

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

                if (hackIcon != null)
                    hackIcon.enabled = true;

                Debug.Log("Hack is ready again!");
            }
        }

        // Activate hack
        if (hackReady && (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0)))
        {
            if (enemy != null)
            {
                enemy.HackEnemy();

                hackReady = false;

                if (hackIcon != null)
                    hackIcon.enabled = false;

                Debug.Log("Hack Activated");
            }
            else
            {
                Debug.LogError("Enemy not assigned!");
            }
        }
    }
}