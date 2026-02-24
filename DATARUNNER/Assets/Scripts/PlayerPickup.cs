using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            DataChip chip = hit.collider.GetComponent<DataChip>();

            if (chip != null)
            {
                chip.PickUp();
            }
        }
    }
}