using UnityEngine;

public class TopPressMovement : MonoBehaviour
{
    [SerializeField] private GameObject TopPlunger;
    [SerializeField] private float slamDistance = 5f; // Distance to slam down
    [SerializeField] private float slamSpeed = 10f;   // Speed of the slam

    private Vector3 initialPosition;
    private bool isSlamming = false;
    private bool isReturning = false;
    private bool hasSlammed = false;

    void Start()
    {
        initialPosition = TopPlunger.transform.position;
    }

    void Update()
    {
        if (isSlamming)
        {
            SlamDown();
        }
        else if (isReturning)
        {
            ReturnUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasSlammed)
        {
            Debug.Log("Player entered trigger. Slamming down.");
            isSlamming = true;
            hasSlammed = true;
        }
    }

    private void SlamDown()
    {
        Vector3 targetPosition = initialPosition + Vector3.down * slamDistance;
        TopPlunger.transform.position = Vector3.MoveTowards(TopPlunger.transform.position, targetPosition, slamSpeed * Time.deltaTime);

        if (TopPlunger.transform.position == targetPosition)
        {
            isSlamming = false; // Stop slamming once the target position is reached
            isReturning = true; // Start returning upwards
        }
    }

    private void ReturnUp()
    {
        TopPlunger.transform.position = Vector3.MoveTowards(TopPlunger.transform.position, initialPosition, slamSpeed * Time.deltaTime);

        if (TopPlunger.transform.position == initialPosition)
        {
            isReturning = false; // Stop returning once back at the initial position
            hasSlammed = false; // Reset to allow future slams
        }
    }
}
