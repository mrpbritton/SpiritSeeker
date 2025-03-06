using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent thisEnemy;

    private void Awake()
    {
        thisEnemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target)
        {
            thisEnemy.destination = target.position;
        }
    }
}
