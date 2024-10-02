using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldSphere; // Reference to the shield GameObject

    // Method to deactivate the shield
    public void TurnOffShield()
    {
        if (shieldSphere != null)
        {
            shieldSphere.SetActive(false);
            Debug.Log("Boss shield deactivated!");
        }
        else
        {
            Debug.LogWarning("ShieldSphere reference not set on BossShield script.");
        }
    }

    public void TurnOnShield()
    {
        if (shieldSphere != null)
        {
            shieldSphere.SetActive(true);
            Debug.Log("Boss shield reactivated!");
        }
    }
}
