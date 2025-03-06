using UnityEngine;
using UnityEngine.Events;

public class HPController : MonoBehaviour
{
    public Healthbar healthbar;

    private HealthVolume hpChange;

    // Player Health Related Values
    public float maxHP = 100f;
    public float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("healthVolume"))
        {
            if(other.TryGetComponent<HealthVolume>(out hpChange))
            {
                UpdateHealth(hpChange.amount);
            }
        }
    }

    public void UpdateHealth(float amountOfHP)
    {
        currentHP = Mathf.Clamp(currentHP + amountOfHP, 0, maxHP);
        healthbar.UpdateHPBar(currentHP, maxHP);
    }
}
