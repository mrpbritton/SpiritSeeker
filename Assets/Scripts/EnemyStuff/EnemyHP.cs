using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 10;
    public float criticalHP = 3;
    public Material lowHealthMaterial;

    public UnityEvent EnemyDeath;

    private Renderer mRenderer;
    private float currentHP;
    private bool canTakeDamage = true;
    private float damageCD = 0.25f;

    private void OnEnable()
    {
        currentHP = maxHP;
        mRenderer = GetComponent<Renderer>();
    }

    public void UpdateHealth(float amount)
    {
        if(canTakeDamage)
        {
            currentHP += amount;
            canTakeDamage = false;
            StartCoroutine(nameof(DamageCD));
            if(currentHP <= criticalHP)
            {
                mRenderer.material = lowHealthMaterial;
            }
            if (currentHP <= 0)
            {
                EnemyDeath.Invoke();
            }
        }
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator DamageCD()
    {
        while (!canTakeDamage)
        {
            yield return new WaitForSeconds(damageCD);
            canTakeDamage = true;
            StopCoroutine(nameof(DamageCD));
        }
    }
}
