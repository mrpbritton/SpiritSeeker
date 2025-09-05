using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTrigger : MonoBehaviour
{
    [SerializeField] private bool mustReactivate = false;
    [SerializeField] private float reactivationCooldownSeconds = 3f;

    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private ActivatePowerUp powerUpBehavior;
    [SerializeField] private List<MeshRenderer> renderers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUpController powerUpController = other.GetComponentInChildren<PowerUpController>();
            if (powerUpController != null && powerUpController.canNavigate == false)
            {
                powerUpController.navigationArrow.makeArrowVisible();
                powerUpController.beginNavigation();
                Deactivate();
                if (mustReactivate)
                {
                    StartCoroutine(nameof(ReactivationCooldown));
                }
            }
            else if (powerUpController != null && powerUpController.canNavigate == true)
            {
                powerUpController.navigationCurrentCDTime += powerUpController.navigationCooldownSeconds;
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
