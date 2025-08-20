using UnityEngine;
using UnityEngine.Events;

public class XpPowerup : MonoBehaviour
{
    public float xpValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            XPBar player = other.GetComponentInChildren<XPBar>();
            if (player != null)
            {
                player.XPGained(xpValue);
                DeactivateSelf();
            }
        }
    }

    private void DeactivateSelf()
    {
        this.gameObject.SetActive(false);
    }
}
