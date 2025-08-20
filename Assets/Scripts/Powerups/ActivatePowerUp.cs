using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePowerUp : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> powerUpMeshes;

    public Transform target;

    public void Activate()
    {
        StartCoroutine(LookingAtPlayer());
        foreach (MeshRenderer mesh in powerUpMeshes)
        {
            if (mesh != null)
            {
                mesh.enabled = true;
            }
        }
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        foreach (MeshRenderer mesh in powerUpMeshes)
        {
            if (mesh != null)
            {
                mesh.enabled = false;
            }
        }
    }

    IEnumerator LookingAtPlayer()
    {
        while (target)
        {
            Vector3 direction = target.position - transform.position;
            direction.y += 1;
            transform.rotation = Quaternion.LookRotation(direction);

            yield return new WaitForEndOfFrame();
        }
    }
}
