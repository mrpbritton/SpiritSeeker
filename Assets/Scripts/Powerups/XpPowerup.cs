using UnityEngine;
using UnityEngine.Events;

public class XpPowerup : MonoBehaviour
{
    public UnityEvent xpActivated;
    public float xpValue = 10;

    private EyeTracking eyeTracking;

    private void OnEnable()
    {
        eyeTracking = GetComponentInChildren<EyeTracking>();
    }

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
        eyeTracking.enabled = false;
        this.gameObject.SetActive(false);
    }
}
