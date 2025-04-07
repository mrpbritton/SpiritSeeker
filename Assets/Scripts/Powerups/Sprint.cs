using UnityEngine;
using UnityEngine.Events;

public class Sprint : MonoBehaviour
{
    public UnityEvent sprintActivated;

    private EyeTracking eyeTracking;

    private void OnEnable()
    {
        eyeTracking = GetComponentInChildren<EyeTracking>();  
    }

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
                    eyeTracking.enabled = false;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
