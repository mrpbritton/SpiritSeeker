using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float healAmount = 10;

    [SerializeField] private bool mustReactivate = false;
    [SerializeField] private float reactivationCooldownSeconds = 3f;

    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private ActivatePowerUp powerUpBehavior;
    [SerializeField] private List<MeshRenderer> renderers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HPController>(out HPController player))
        {
            if (player.currentHP < player.maxHP)
            {
                player.UpdateHealth(healAmount);
                Deactivate();
                if (mustReactivate)
                {
                    StartCoroutine(nameof(ReactivationCooldown));
                }
            }
        }
    }

    private void Deactivate()
    {
        sphereCollider.enabled = false;
        powerUpBehavior.enabled = false;
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.enabled)
            {
                renderer.enabled = false;
            }
        }
    }

    private void Reactivate()
    {
        sphereCollider.enabled = true;
        powerUpBehavior.enabled = true;
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.enabled)
            {
                renderer.enabled = true;
            }
        }
    }

    private IEnumerator ReactivationCooldown()
    {
        yield return new WaitForSeconds(reactivationCooldownSeconds);
        Reactivate();
        StopAllCoroutines();
    }
}
