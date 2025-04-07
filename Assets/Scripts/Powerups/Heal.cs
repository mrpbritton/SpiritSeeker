using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount = 10;

    private EyeTracking eyeTracking;

    private void OnEnable()
    {
        eyeTracking = GetComponentInChildren<EyeTracking>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HPController>(out HPController player))
        {
            if (player.currentHP < player.maxHP)
            {
                player.UpdateHealth(healAmount);
                eyeTracking.enabled = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
