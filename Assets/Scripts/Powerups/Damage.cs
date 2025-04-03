using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public UnityEvent damageActivated;

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
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
