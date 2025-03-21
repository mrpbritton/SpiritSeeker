using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCombat>(out PlayerCombat player))
        {
            player.DamageBoost();
            this.gameObject.SetActive(false);
        }
    }
}
