using UnityEngine;
using UnityEngine.UI;

public class EnemyAlertUI : MonoBehaviour
{
    public Image alertImage;

    public Sprite hiddenSprite;
    public Sprite detectedSprite;

    public void SetDetected(bool detected)
    {
        if (detected)
            alertImage.sprite = detectedSprite;
        else
            alertImage.sprite = hiddenSprite;
    }
}