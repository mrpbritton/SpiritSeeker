using UnityEngine;
using UnityEngine.Events;

public class Sprint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.TryGetComponent<PlayerMove>(out PlayerMove player))
            {
                if (player.canSprint == true)
                {
                    Debug.Log("Sprint powerup already active, adding cooldown time.");
                    player.powerUpController.sprintCurrentCDTime += player.powerUpController.sprintCooldownSeconds;
                    DeactivateSelf();
                }
                if (player.canSprint == false)
                {
                    player.sprintNowActive();
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
