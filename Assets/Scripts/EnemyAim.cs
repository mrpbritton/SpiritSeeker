using System.Collections;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform target;

    private Vector3 distanceToPlayer;
    private float distanceCondensed;

    private void OnEnable()
    {
        StartCoroutine(nameof(Aiming));
        StartCoroutine(nameof(recordDistance));
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

    IEnumerator recordDistance()
    {
        while (true)
        {
            distanceToPlayer = transform.position - target.position;
            distanceCondensed = Mathf.Abs(distanceToPlayer.x) + Mathf.Abs(distanceToPlayer.z);
            Debug.Log(distanceCondensed);

            yield return new WaitForSeconds(1);
        }
    }
}
