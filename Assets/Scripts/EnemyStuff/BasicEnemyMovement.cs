using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour
{
    public Transform target;
    public float detectionDistance = 100;

    private NavMeshAgent thisEnemy;
    private Vector3 distanceToPlayer;
    private float distanceCondensed;

    private void Awake()
    {
        thisEnemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        distanceToPlayer = transform.position - target.position;
        distanceCondensed = Mathf.Abs(distanceToPlayer.x) + Mathf.Abs(distanceToPlayer.z);

        if (distanceCondensed <= detectionDistance)
        {
            thisEnemy.destination = target.position;
        }
    }
}
