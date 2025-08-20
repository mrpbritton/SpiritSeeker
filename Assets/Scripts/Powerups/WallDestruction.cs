using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUpController powerUpController = other.GetComponentInChildren<PowerUpController>();
            if (powerUpController != null && powerUpController.canDestroyWalls == false)
            {
                powerUpController.beginWallDestruction();
                DeactivateSelf();
            }
            else if (powerUpController != null && powerUpController.canDestroyWalls == true)
            {
                powerUpController.wallDestructionCurrentCDTime += powerUpController.wallDestructionCooldownSeconds;
                DeactivateSelf();
            }
        }
    }

    private void DeactivateSelf()
    {
        this.gameObject.SetActive(false);
    }
}
