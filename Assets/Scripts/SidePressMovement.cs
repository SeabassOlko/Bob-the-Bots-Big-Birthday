using UnityEngine;

public class SidePressMovement : MonoBehaviour
{
    [SerializeField] private GameObject leftPlunger;  // Left plunger object
    [SerializeField] private GameObject rightPlunger; // Right plunger object
    [SerializeField] private float slamDistance = 5f; // Distance each plunger moves towards the center
    [SerializeField] private float slamSpeed = 10f;   // Speed of the slamming action

    private Vector3 leftPlungerInitialPosition;
    private Vector3 rightPlungerInitialPosition;
    private bool isSlamming = false;
    private bool isReturning = false;
    private bool hasSlammed = false;

    void Start()
    {
        leftPlungerInitialPosition = leftPlunger.transform.position;
        rightPlungerInitialPosition = rightPlunger.transform.position;
    }

    void Update()
    {
        if (isSlamming)
        {
            SlamInwards();
        }
        else if (isReturning)
        {
            ReturnOutwards();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasSlammed)
        {
            Debug.Log("Player entered trigger. Slamming towards the center.");
            isSlamming = true;
            hasSlammed = true;
        }
    }

    private void SlamInwards()
    {
        // Calculate target positions for the plungers to meet in the middle
        Vector3 leftTargetPosition = leftPlungerInitialPosition + Vector3.right * slamDistance;
        Vector3 rightTargetPosition = rightPlungerInitialPosition + Vector3.left * slamDistance;

        // Move both plungers towards the target positions
        leftPlunger.transform.position = Vector3.MoveTowards(leftPlunger.transform.position, leftTargetPosition, slamSpeed * Time.deltaTime);
        rightPlunger.transform.position = Vector3.MoveTowards(rightPlunger.transform.position, rightTargetPosition, slamSpeed * Time.deltaTime);

        // Check if both plungers have reached their target positions
        if (leftPlunger.transform.position == leftTargetPosition && rightPlunger.transform.position == rightTargetPosition)
        {
            isSlamming = false; // Stop slamming once both plungers reach the center
            isReturning = true; // Start returning outwards
        }
    }

    private void ReturnOutwards()
    {
        // Move both plungers back to their initial positions
        leftPlunger.transform.position = Vector3.MoveTowards(leftPlunger.transform.position, leftPlungerInitialPosition, slamSpeed * Time.deltaTime);
        rightPlunger.transform.position = Vector3.MoveTowards(rightPlunger.transform.position, rightPlungerInitialPosition, slamSpeed * Time.deltaTime);

        // Check if both plungers have returned to their initial positions
        if (leftPlunger.transform.position == leftPlungerInitialPosition && rightPlunger.transform.position == rightPlungerInitialPosition)
        {
            isReturning = false; // Stop returning once both plungers are back at their initial positions
            hasSlammed = false; // Reset to allow future slams
        }
    }
}
