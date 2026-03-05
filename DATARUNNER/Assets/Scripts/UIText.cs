using UnityEngine;
// Allows use of TextMeshPro
using TMPro;

// attach to canvas 

public class UIText : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component
    [SerializeField] private TextMeshProUGUI m_MyText;

    public void UpdateObjectiveText(string newObjective)
    {
        if (m_MyText != null)
        {
            m_MyText.text = newObjective;
        }
    }
}