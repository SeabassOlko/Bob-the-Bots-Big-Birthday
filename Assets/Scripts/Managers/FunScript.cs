using UnityEngine;
using TMPro;

public class FunScript : MonoBehaviour
{
    public TextMeshProUGUI progressText; // TextMeshPro component
    private float progress = 0;

    void Update()
    {
        if (progress < 100)
        {
            progress += Time.deltaTime / 3; // Adjust speed as needed
            progressText.text = $"{(int)progress}% complete";
        }
    }
}
