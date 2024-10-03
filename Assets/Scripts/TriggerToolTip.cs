using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextOnTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField]private string enterText = "You have entered the area!";
    [SerializeField] private string exitText = "";

    [SerializeField] private GameObject bgColor;

    private void Start()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = exitText;  // Set initial text to empty or default message
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textMeshPro != null)
        {
            textMeshPro.text = enterText;
            bgColor.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && textMeshPro != null)
        {
            textMeshPro.text = exitText;  // Hide text or display different message
            bgColor.SetActive(false);
        }
    }
}
