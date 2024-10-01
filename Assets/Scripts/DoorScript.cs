using UnityEngine;

public class VerticalPressMovement : MonoBehaviour
{
    [SerializeField] private GameObject topPlunger;     // Top plunger object
    [SerializeField] private GameObject bottomPlunger;  // Bottom plunger object
    [SerializeField] private float slamDistance = 5f;   // Distance each plunger moves apart
    [SerializeField] private float slamSpeed = 2f;      // Speed of the movement (lower for visibility)
    [SerializeField] private float waitTime = 3f;       // Time to wait before closing

    private Vector3 topPlungerInitialPosition;
    private Vector3 bottomPlungerInitialPosition;
    private bool isOpening = false;
    private bool isClosing = false;
    private bool hasOpened = false;

    void Start()
    {
        // Set initial positions for both plungers
        topPlungerInitialPosition = topPlunger.transform.position;
        bottomPlungerInitialPosition = bottomPlunger.transform.position;
    }

    void Update()
    {
        if (isOpening)
        {
            OpenPlungers();
        }
        else if (isClosing)
        {
            ClosePlungers();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasOpened)
        {
            Debug.Log("Player entered trigger. Opening plungers.");
            isOpening = true;
            hasOpened = true;
        }
    }

    private void OpenPlungers()
    {
        // Calculate target positions for the plungers to move apart
        Vector3 topTargetPosition = topPlungerInitialPosition + Vector3.up * slamDistance;
        Vector3 bottomTargetPosition = bottomPlungerInitialPosition + Vector3.down * slamDistance;

        // Move both plungers towards the target positions
        topPlunger.transform.position = Vector3.MoveTowards(topPlunger.transform.position, topTargetPosition, slamSpeed * Time.deltaTime);
        bottomPlunger.transform.position = Vector3.MoveTowards(bottomPlunger.transform.position, bottomTargetPosition, slamSpeed * Time.deltaTime);

        // Log current positions
        Debug.Log($"Top Plunger Position: {topPlunger.transform.position}, Target: {topTargetPosition}");
        Debug.Log($"Bottom Plunger Position: {bottomPlunger.transform.position}, Target: {bottomTargetPosition}");

        // Check if both plungers have reached their target positions
        if (topPlunger.transform.position == topTargetPosition && bottomPlunger.transform.position == bottomTargetPosition)
        {
            isOpening = false; // Stop opening once both plungers reach their target positions
            StartCoroutine(WaitBeforeClosing());
        }
    }

    private System.Collections.IEnumerator WaitBeforeClosing()
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waiting completed. Closing plungers.");
        isClosing = true;
    }

    private void ClosePlungers()
    {
        // Move both plungers back to their initial positions
        topPlunger.transform.position = Vector3.MoveTowards(topPlunger.transform.position, topPlungerInitialPosition, slamSpeed * Time.deltaTime);
        bottomPlunger.transform.position = Vector3.MoveTowards(bottomPlunger.transform.position, bottomPlungerInitialPosition, slamSpeed * Time.deltaTime);

        // Log current positions
        Debug.Log($"Closing - Top Plunger Position: {topPlunger.transform.position}");
        Debug.Log($"Closing - Bottom Plunger Position: {bottomPlunger.transform.position}");

        // Check if both plungers have returned to their initial positions
        if (topPlunger.transform.position == topPlungerInitialPosition && bottomPlunger.transform.position == bottomPlungerInitialPosition)
        {
            isClosing = false; // Stop closing once both plungers are back at their initial positions
            hasOpened = false; // Reset to allow future openings
        }
    }
}
