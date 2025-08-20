using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private int contactDamage = -5;
    public Transform target;
    public float inRange = 3;
    public float animationLength = 1;
    public float animationSpeed = 1;

    private BasicEnemyMovement movementScript;
    private bool isInRange = false;
    private Animator animator;
    private Vector3 distanceToPlayer;
    private float distanceCondensed;

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        movementScript = GetComponent<BasicEnemyMovement>();
        target = movementScript.target;
    }

    private void Update()
    {
        distanceToPlayer = transform.position - target.position;
        distanceCondensed = Mathf.Abs(distanceToPlayer.x) + Mathf.Abs(distanceToPlayer.z);

        if (distanceCondensed <= inRange && !isInRange)
        {
            StartCoroutine(nameof(Attacking));
            isInRange = true;
        }
        else if(distanceCondensed > inRange && isInRange)
        {
            StopCoroutine(nameof(Attacking));
            animator.Play("EnemySwordIdle");
            isInRange = false;
        }
    }

    // Damage player on contact
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HPController>().UpdateHealth(contactDamage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HPController>().UpdateHealth(contactDamage);
        }
    }

    private IEnumerator Attacking()
    {
        while (true)
        {
            animator.speed = animationSpeed;
            animator.Play("SwordSwing");
            yield return new WaitForSeconds(animationLength * animationSpeed);
        }
    }
}
