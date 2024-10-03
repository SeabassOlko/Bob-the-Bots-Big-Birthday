using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeScript : MonoBehaviour
{
    [Range(0, 360)]
    [SerializeField] float viewAngle;

    [SerializeField] float viewRadius;

    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] Transform pointOfView;

    bool canSeeTarget = false;
    public GameObject rangetarget;

    // Update is called once per frame

    void Start()
    {
        if (pointOfView == null)
        {
            pointOfView = transform;
        }
    }
    void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] playerChecks = Physics.OverlapSphere(pointOfView.position, viewRadius, targetMask);

        if (playerChecks.Length != 0)
        {
            Transform playerTarget = playerChecks[0].transform;
            Vector3 directionToPlayer = (playerTarget.position - pointOfView.position).normalized;

            if (Vector3.Angle(pointOfView.forward, directionToPlayer) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(pointOfView.position, playerTarget.position);

                if (!Physics.Raycast(pointOfView.position, directionToPlayer, distanceToTarget, obstructionMask))
                    canSeeTarget = true;
                else
                    canSeeTarget = false;

            }
            else
                canSeeTarget = false;
        }
        else if (canSeeTarget)
            canSeeTarget = false;
    }

    public bool SeeTarget()
    {
        return canSeeTarget;
    }

    private void OnDrawGizmos()
    {
        // Draw the view radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointOfView.position, viewRadius);

        // Draw the view angles
        Vector3 angleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 angleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pointOfView.position, pointOfView.position + angleA * viewRadius);
        Gizmos.DrawLine(pointOfView.position, pointOfView.position + angleB * viewRadius);

        // If the enemy can see the player, draw a line to the player
        if (canSeeTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointOfView.position, rangetarget.transform.position);
        }
    }

    // Helper function to convert angle to a direction vector
    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += pointOfView.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
