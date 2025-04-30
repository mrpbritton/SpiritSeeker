using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HPController : MonoBehaviour
{
    public UnityEvent Dead;
    public Healthbar healthbar;
    public float iFramesTime = 0.5f;

    private bool canBeDamaged = true;
    private bool isRegenerating = true;
    private bool regenOnCooldown = false;
    private HealthVolume hpChange;

    // Player Health Related Values
    public float maxHP = 100f;
    public float currentHP;
    public float regenAmount = 1f;
    public float regenFrequency = 1f;
    public float regenCooldownTime = 5f;

    private void Awake()
    {
        currentHP = maxHP;
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(RegenerateHealth));
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
        // Take Damage
        if (amountOfHP < 0 && canBeDamaged)
        {
            canBeDamaged = false;
            StartCoroutine(nameof(IFrames));
            currentHP = Mathf.Clamp(currentHP + amountOfHP, 0, maxHP);
            healthbar.UpdateHPBar(currentHP, maxHP);
            // Stop regenerating if hit
            if (isRegenerating)
            {
                StopCoroutine(nameof(RegenerateHealth));
                isRegenerating = false;
                regenOnCooldown = true;
                StartCoroutine(nameof(RegenerationCooldown));
            }
            // Restart regen cooldown if hit
            else if (regenOnCooldown)
            {
                StopCoroutine(nameof(RegenerationCooldown));
                StartCoroutine(nameof(RegenerationCooldown));
            }
            if (currentHP == 0)
            {
                Dead.Invoke();
            }
        }
        // Receive Healing
        else if(amountOfHP > 0)
        {
            currentHP = Mathf.Clamp(currentHP + amountOfHP, 0, maxHP);
            healthbar.UpdateHPBar(currentHP, maxHP);
            if (currentHP == 0)
            {
                Dead.Invoke();
            }
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    private IEnumerator IFrames()
    {
        yield return new WaitForSeconds(iFramesTime);
        canBeDamaged = true;
        StopCoroutine(nameof(IFrames));
    }

    private IEnumerator RegenerationCooldown()
    {
        yield return new WaitForSeconds(regenCooldownTime);
        StartCoroutine(nameof(RegenerateHealth));
        regenOnCooldown = false;
        isRegenerating = true;
        StopCoroutine(nameof(RegenerationCooldown));
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenFrequency);
            UpdateHealth(regenAmount);
        }
    }
}
