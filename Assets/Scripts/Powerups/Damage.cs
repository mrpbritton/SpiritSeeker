using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public UnityEvent damageActivated;

    private EyeTracking eyeTracking;

    private void OnEnable()
    {
        eyeTracking = GetComponentInChildren<EyeTracking>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.TryGetComponent<PlayerCombat>(out PlayerCombat player))
            {
                if (player.buffed == false)
                {
                    player.DamageBoost(); 
                    PowerUpController HUD = other.GetComponentInChildren<PowerUpController>();
                    HUD.haveDamage();
                    eyeTracking.enabled = false;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
