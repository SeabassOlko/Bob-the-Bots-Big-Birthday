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

    bool canSeeTarget = false;
    public GameObject rangetarget;

    // Update is called once per frame
    void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] playerChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (playerChecks.Length != 0)
        {
            Transform playerTarget = playerChecks[0].transform;
            Vector3 directionToPlayer = (playerTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToTarget, obstructionMask))
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
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // Draw the view angles
        Vector3 angleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 angleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + angleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + angleB * viewRadius);

        // If the enemy can see the player, draw a line to the player
        if (canSeeTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, rangetarget.transform.position);
        }
    }

    // Helper function to convert angle to a direction vector
    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
