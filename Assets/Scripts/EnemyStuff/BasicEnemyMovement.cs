using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour
{
    public Transform target;
    public float detectionDistance = 10;

    private NavMeshAgent thisEnemy;
    private Vector3 distanceToPlayer;
    private float distanceCondensed;
    private Vector3 lookDirection;
    private bool isLookingManually = false;

    private void Awake()
    {
        thisEnemy = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        distanceToPlayer = transform.position - target.position;
        distanceCondensed = Mathf.Sqrt((Mathf.Abs(distanceToPlayer.x) * Mathf.Abs(distanceToPlayer.x)) + (Mathf.Abs(distanceToPlayer.z) * Mathf.Abs(distanceToPlayer.z)));

        if (distanceCondensed <= detectionDistance)
        {
            if(thisEnemy.enabled == false)
            {
                thisEnemy.enabled = true;
            }
            thisEnemy.destination = target.position;
        }
        else
        {
            thisEnemy.enabled = false;
        }

        if(distanceCondensed <= thisEnemy.stoppingDistance * 2)
        {
            StartCoroutine(nameof(LookingAtPlayer));
            isLookingManually = true;
        }
        else
        {
            if (isLookingManually)
            {
                isLookingManually = false;
            }
            StopCoroutine(nameof(LookingAtPlayer));
        }
    }

    IEnumerator LookingAtPlayer()
    {
        while (target)
        {
            lookDirection = target.position - transform.position;
            lookDirection.y += 1;
            transform.rotation = Quaternion.LookRotation(lookDirection);

            yield return new WaitForEndOfFrame();
        }
    }
}
