using UnityEngine;

public class XboxPickup : MonoBehaviour
{
    public float pickupDistance = 3f;
    public Camera playerCamera;

    void Start()
    {
        if (playerCamera == null) playerCamera = Camera.main;
        if (playerCamera == null) Debug.LogError("No camera assigned and no MainCamera found.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
            TryPickup();
    }

    void TryPickup()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            DataChip chip = hit.collider.GetComponentInParent<DataChip>();
           // if (chip != null) chip.PickUp();
        }
    }
}