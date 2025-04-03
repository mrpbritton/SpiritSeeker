using UnityEngine;
using UnityEngine.Events;

public class DoubleJump : MonoBehaviour
{
    public UnityEvent doubleJumpActivated;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if(other.TryGetComponent<PlayerMove>(out PlayerMove player))
            {
                if (player.canDoubleJump == false)
                {
                    player.doubleJumpNowActive();
                    PowerUpController HUD = other.GetComponentInChildren<PowerUpController>();
                    HUD.haveDoubleJump();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
