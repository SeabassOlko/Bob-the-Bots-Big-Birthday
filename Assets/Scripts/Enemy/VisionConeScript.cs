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
}
