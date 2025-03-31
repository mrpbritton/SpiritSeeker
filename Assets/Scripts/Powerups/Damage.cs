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
                    damageActivated.Invoke();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
