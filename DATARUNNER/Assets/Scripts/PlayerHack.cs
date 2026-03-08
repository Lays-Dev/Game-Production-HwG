using UnityEngine;
using UnityEngine.UI;

public class PlayerHack : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public LayerMask hackLayers;

    [Header("Enemy Hack")]
    public float enemyHackDistance = 20f;

    [Header("Hack Point")]
    public float hackPointDistance = 3f;
    public float hackHoldTime = 3f;

    [Header("Ability Cooldown")]
    public float firstUnlockDelay = 15f;
    public float hackCooldown = 60f;

    [Header("UI")]
    public Image hackIcon;

    private float unlockTimer = 0f;
    private float cooldownTimer = 0f;

    private bool hackUnlocked = false;
    private bool hackReady = false;

    private float holdTimer = 0f;

    void Update()
    {
        HandleCooldown();

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(playerCamera.transform.position,
                    playerCamera.transform.forward * enemyHackDistance,
                    Color.red);

        if (Physics.Raycast(ray, out hit, enemyHackDistance, hackLayers))
        {
            Debug.Log(hit.collider.name);
            // ENEMY
            EnemyHack enemy = hit.collider.GetComponentInParent<EnemyHack>();

            if (enemy != null)
            {
                if (hackReady && (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0)))
                {
                    enemy.HackEnemy();
                    hackReady = false;

                    if (hackIcon != null)
                        hackIcon.enabled = false;

                    Debug.Log("Enemy hacked!");
                }

                return;
            }

            // HACK BOX
            HackableButton hackPoint = hit.collider.GetComponentInParent<HackableButton>();

            if (hackPoint != null && hit.distance <= hackPointDistance)
            {
                if (Input.GetKey(KeyCode.Q) || Input.GetMouseButton(0))
                {
                    holdTimer += Time.deltaTime;

                    Debug.Log($"Hacking {hackPoint.name}: {holdTimer:F2}/{hackHoldTime}");

                    if (holdTimer >= hackHoldTime)
                    {
                        hackPoint.Hack();
                        holdTimer = 0f;
                    }
                }
                else
                {
                    holdTimer = 0f;
                }

                return;
            }
        }
    }

    void HandleCooldown()
    {
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

        if (!hackReady)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= hackCooldown)
            {
                hackReady = true;
                cooldownTimer = 0f;

                if (hackIcon != null)
                    hackIcon.enabled = true;

                Debug.Log("Hack ready again!");
            }
        }
    }
}