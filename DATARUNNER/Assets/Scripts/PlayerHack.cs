using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

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

    [Header("Hack Lock")]
    private bool hackCompleted = false;

    [Header("Ability Cooldown")]
    public float firstUnlockDelay = 15f;
    public float hackCooldown = 60f;

    [Header("UI")]
    public Image hackIcon;
    public TMP_Text hackTimerText;

    private float unlockTimer = 0f;
    private float cooldownTimer = 0f;

    private bool hackUnlocked = false;
    private bool hackReady = false;

    private float holdTimer = 0f;

    // Track current hack target
    private HackableButton currentHackTarget = null;

    private PlayerControls3 controls;
    private bool hackPressed = false;

    void Awake()
    {
        controls = new PlayerControls3();

        controls.Player.Hack.performed += ctx => hackPressed = true;
        controls.Player.Hack.canceled += ctx =>
        {
            hackPressed = false;
            hackCompleted = false; // allow hacking again after release
        };
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

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

            // ENEMY HACK
            EnemyHack enemy = hit.collider.GetComponentInParent<EnemyHack>();

            if (enemy != null)
            {
                if (hackReady && hackPressed)
                {
                    enemy.HackEnemy();
                    hackReady = false;

                    if (hackIcon != null)
                        hackIcon.enabled = false;

                    Debug.Log("Enemy hacked!");
                }

                ResetHackProgress();
                return;
            }

            // HACK POINT
            HackableButton hackPoint = hit.collider.GetComponentInParent<HackableButton>();

            if (hackPoint != null && hit.distance <= hackPointDistance)
            {
                // If player switches targets, reset timer
                if (currentHackTarget != hackPoint)
                {
                    ResetHackProgress();
                    currentHackTarget = hackPoint;
                }

                if (hackPressed && !hackCompleted)
                {
                    holdTimer += Time.deltaTime;

                    float remainingTime = hackHoldTime - holdTimer;

                    if (hackTimerText != null)
                        hackTimerText.text = "Hacking: " + remainingTime.ToString("F1") + "s";

                    Debug.Log($"Hacking {hackPoint.name}: {holdTimer:F2}/{hackHoldTime}");

                    if (holdTimer >= hackHoldTime)
                    {
                        hackPoint.Hack();

                        holdTimer = 0f;
                        hackCompleted = true;

                        if (hackTimerText != null)
                            hackTimerText.text = "";

                        Debug.Log("Hack Complete");
                    }
                }

                if (!hackPressed)
                {
                    holdTimer = 0f;

                    if (hackTimerText != null)
                        hackTimerText.text = "";
                }

                return;
            }
        }

        ResetHackProgress();
    }

    void ResetHackProgress()
    {
        holdTimer = 0f;
        currentHackTarget = null;

        if (hackTimerText != null)
            hackTimerText.text = "";
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