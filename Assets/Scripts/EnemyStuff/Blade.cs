using UnityEngine;

public class Blade : MonoBehaviour
{
    public float damage = -10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HPController>(out HPController player))
        {
            player.UpdateHealth(damage);
        }
    }
}
