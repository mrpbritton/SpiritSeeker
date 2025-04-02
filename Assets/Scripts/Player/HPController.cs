using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HPController : MonoBehaviour
{
    public UnityEvent Dead;
    public Healthbar healthbar;
    public float iFramesTime = 0.5f;

    private bool canBeDamaged = true;
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
        if (amountOfHP < 0 && canBeDamaged)
        {
            canBeDamaged = false;
            StartCoroutine(nameof(IFrames));
            currentHP = Mathf.Clamp(currentHP + amountOfHP, 0, maxHP);
            healthbar.UpdateHPBar(currentHP, maxHP);
            if (currentHP == 0)
            {
                Dead.Invoke();
            }
        }
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
}
