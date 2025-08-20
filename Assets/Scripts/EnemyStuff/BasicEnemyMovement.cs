using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour
{
    public Transform target;
    // public float detectionDistance = 10;

    [SerializeField] private NavMeshAgent thisEnemy;
    [SerializeField] private List<MeshRenderer> enemyMeshes;
    [SerializeField] private GameObject weapon;
    [SerializeField] private EnemyMelee enemyMelee;
    [SerializeField] private EnemyAim enemyAim;

    // private Vector3 distanceToPlayer;
    // private float distanceCondensed;
    // private Vector3 lookDirection;
    // private bool isLookingManually = false;

    public void Activate()
    {
        thisEnemy.enabled = true;
        foreach (MeshRenderer enemyMesh in enemyMeshes)
        {
            enemyMesh.enabled = true;
        }
        weapon.SetActive(true);
        if (enemyMelee != null)
        {
            enemyMelee.enabled = true;
        }
        else if (enemyAim != null)
        {
            enemyAim.enabled = true;
        }
        StartCoroutine(MoveTowardsPlayer());
        StartCoroutine(LookingAtPlayer());
    }

    public void Deactivate()
    {
        thisEnemy.enabled = false;
        foreach (MeshRenderer enemyMesh in enemyMeshes)
        {
            enemyMesh.enabled = false;
        }
        weapon.SetActive(false);
        if (enemyMelee != null)
        {
            enemyMelee.enabled = false;
        }
        else if (enemyAim != null)
        {
            enemyAim.enabled = false;
        }
        StopAllCoroutines();
    }

    private IEnumerator MoveTowardsPlayer()
    {
        while (true)
        {
            thisEnemy.destination = target.position;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator LookingAtPlayer()
    {
        while (true)
        {
            Vector3 lookDirection = target.position - transform.position;
            lookDirection.y += 1;
            transform.rotation = Quaternion.LookRotation(lookDirection);

            yield return new WaitForEndOfFrame();
        }
    }

    // --- Old Version --- 
    /* private void OnEnable()
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
    } */
}
