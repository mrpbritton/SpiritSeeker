using System.Collections;
using UnityEngine;

public class EyeTracking : MonoBehaviour
{
    private BoxCollider activeRange;
    private Transform target = null;
    private Vector3 direction;

    private void OnEnable()
    {
        activeRange = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && target == null && this.enabled == true)
        {
            target = other.transform;
            StartCoroutine(LookingAtPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(LookingAtPlayer());
            target = null;
        }
    }

    IEnumerator LookingAtPlayer()
    {
        while (target)
        {
            direction = target.position - transform.position;
            direction.y += 1;
            transform.rotation = Quaternion.LookRotation(direction);
            yield return new WaitForEndOfFrame();
        }
    }
}
