using UnityEngine;

public class NavigationTrigger : MonoBehaviour
{
    private EyeTracking eyeTracking;

    private void OnEnable()
    {
        eyeTracking = GetComponentInChildren<EyeTracking>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUpController powerUpController = other.GetComponentInChildren<PowerUpController>();
            if (powerUpController != null && powerUpController.canNavigate == false)
            {
                powerUpController.navigationArrow.makeArrowVisible();
                powerUpController.beginNavigation();
                DeactivateSelf();
            }
            else if (powerUpController != null && powerUpController.canNavigate == true)
            {
                powerUpController.navigationCurrentCDTime += powerUpController.navigationCooldownSeconds;
                DeactivateSelf();
            }
        }
    }

    private void DeactivateSelf()
    {
        eyeTracking.enabled = false;
        this.gameObject.SetActive(false);
    }
}
