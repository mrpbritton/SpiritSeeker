using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    [SerializeField] private bool mustReactivate = false;
    [SerializeField] private float reactivationCooldownSeconds = 3f;

    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private ActivatePowerUp powerUpBehavior;
    [SerializeField] private List<MeshRenderer> renderers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.TryGetComponent<PlayerCombat>(out PlayerCombat player))
            {
                if (player.buffed == true)
                {
                    player.powerUpController.damageCurrentCDTime += player.powerUpController.damageCooldownSeconds;
                    Deactivate();
                    if (mustReactivate)
                    {
                        StartCoroutine(nameof(ReactivationCooldown));
                    }
                }
                if (player.buffed == false)
                {
                    player.DamageBoost();
                    Deactivate();
                    if (mustReactivate)
                    {
                        StartCoroutine(nameof(ReactivationCooldown));
                    }
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
