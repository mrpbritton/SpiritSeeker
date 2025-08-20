using UnityEngine;
using UnityEngine.Events;

public class DoubleJump : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if(other.TryGetComponent<PlayerMove>(out PlayerMove player))
            {
                player.doubleJumpNowActive();
                PowerUpController HUD = other.GetComponentInChildren<PowerUpController>();
                HUD.acquiredDoubleJump();
                this.gameObject.SetActive(false);
            }
        }
    }
}
