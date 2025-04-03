using UnityEngine;
using UnityEngine.Events;

public class Sprint : MonoBehaviour
{
    public UnityEvent sprintActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.TryGetComponent<PlayerMove>(out PlayerMove player))
            {
                if (player.canSprint == false)
                {
                    player.sprintNowActive(); 
                    PowerUpController HUD = other.GetComponentInChildren<PowerUpController>();
                    HUD.haveSprint();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
