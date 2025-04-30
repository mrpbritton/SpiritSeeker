using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform boltSpawnPoint;
    public float fireRate = 1;
    public float boltSpeed = 5;
    public float attackRange = 30;
    public float boltsHeld = 5;
    public GameObject boltPrefab;

    private Transform target;
    private Vector3 distanceToPlayer;
    private float distanceCondensed;
    private List<Bolt> bolts = new List<Bolt>();
    private bool inRange = false;
    private bool closeRange = false;
    private BasicEnemyMovement movementScript;

    private void OnEnable()
    {
        // Use the same target for aiming as moving
        movementScript = GetComponent<BasicEnemyMovement>();
        target = movementScript.target;

        StartCoroutine(nameof(Aiming));
        StartCoroutine(nameof(Shooting));

        // Instantiate the bolts
        for (int i = 0; i < boltsHeld; i++)
        {
            // A bullet gets instantiated and added to the list of bullets
            bolts.Add(Instantiate(boltPrefab).GetComponent<Bolt>());
        }

        // Disable them to start
        foreach (Bolt bolt in bolts)
        {
            bolt.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        distanceToPlayer = transform.position - target.position;
        distanceCondensed = Mathf.Abs(distanceToPlayer.x) + Mathf.Abs(distanceToPlayer.z);

        if(distanceCondensed <= attackRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    public void StopAiming()
    {
        StopAllCoroutines();
    }

    IEnumerator Aiming()
    {
        Vector3 direction;

        while (true)
        {
            direction = target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            if (inRange)
            {
                // Start firing bolts from the list of held bolts
                foreach (Bolt bolt in bolts)
                {

                    // Only fire inactive bolts
                    if (!bolt.isActive)
                    {
                        bolt.gameObject.SetActive(true);
                        if (distanceCondensed <= 10 && closeRange == false)
                        {
                            closeRange = true;
                            bolt.damage = bolt.damage / 2;
                        }
                        else if (distanceCondensed >= 10 && closeRange == true)
                        {
                            closeRange = false;
                            bolt.damage = bolt.damage * 2;
                        }
                        bolt.transform.position = boltSpawnPoint.position;
                        bolt.Fire(boltSpeed, transform.forward);
                        bolt.transform.rotation = Quaternion.LookRotation(transform.forward);
                        break;
                    }
                }
            }
        }
    }
}
