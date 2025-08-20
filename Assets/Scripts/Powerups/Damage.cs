using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.TryGetComponent<PlayerCombat>(out PlayerCombat player))
            {
                if (player.buffed == true)
                {
                    player.powerUpController.damageCurrentCDTime += player.powerUpController.damageCooldownSeconds;
                    DeactivateSelf();
                }
                if (player.buffed == false)
                {
                    player.DamageBoost(); 
                    DeactivateSelf();
                }
            }
        }
    }

    private void DeactivateSelf()
    {
        this.gameObject.SetActive(false);
    }
}
