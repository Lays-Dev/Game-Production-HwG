using UnityEngine;

public class HackPointPlayer : MonoBehaviour
{
    public float hackTime = 3f;
    public float hackDistance = 3f;

    private float hackTimer = 0f;
    private HackableButton currentTarget;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hackDistance))
        {
            HackableButton hackable = hit.collider.GetComponent<HackableButton>();

            if (hackable != null)
            {
                currentTarget = hackable;

                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Q))
                {
                    hackTimer += Time.deltaTime;

                    if (hackTimer >= hackTime)
                    {
                        currentTarget.Hack();
                        hackTimer = 0f;
                    }
                }
                else
                {
                    hackTimer = 0f;
                }

                return;
            }
        }

        currentTarget = null;
        hackTimer = 0f;
    }
}