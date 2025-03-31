using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HPController>(out HPController player))
        {
            if (player.currentHP < player.maxHP)
            {
                player.UpdateHealth(healAmount);
                this.gameObject.SetActive(false);
            }
        }
    }
}
