using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;

    Transform target;
    NavMeshAgent agent;

    bool stopMovement = false;
    public bool isDead = false;

    public enum EnemyState
    {
        Chase, Patrol, Idle
    }

    public EnemyState enemyState;

    public GameObject[] path;
    public int pathIndex;
    public float distThreshold;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (distThreshold <= 0) distThreshold = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        if (stopMovement) return;
        if (!playerTransform) return;
        if (enemyState == EnemyState.Idle) return;

        if (enemyState == EnemyState.Patrol)
        {
            if (agent.remainingDistance < distThreshold)
            {
                pathIndex++;
                pathIndex %= path.Length;

                target = path[pathIndex].transform;
            }
        }

        if (enemyState == EnemyState.Chase)
            target = playerTransform;

        Debug.DrawLine(transform.position, target.position, Color.red);


        agent.SetDestination(target.position);
    }

    public void chase()
    {
        enemyState = EnemyState.Chase;
    }

    public void patrol()
    {
        enemyState = EnemyState.Patrol;
    }

    public void Idle()
    {
        enemyState = EnemyState.Idle;
    }

    public void stopMove()
    {
        agent.isStopped = true;
    }

    public void startMove()
    {
        agent.isStopped = false;
    }

    public float Speed()
    {
        return agent.velocity.magnitude;
    }
}
