using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 10;
    public float criticalHP = 3;
    public Material lowHealthMaterial;
    public Material baseMaterial;
    public GameObject xpDrop;

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
            StartCoroutine(nameof(DamagedVisual));
            if (currentHP <= 0)
            {
                EnemyDeath.Invoke();
            }
        }
    }

    public void Die()
    {
        Instantiate(xpDrop, transform.position, transform.rotation);
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

    private IEnumerator DamagedVisual()
    {
        mRenderer.material = lowHealthMaterial;
        yield return new WaitForSeconds(0.25f);
        mRenderer.material = baseMaterial;
        StopCoroutine(nameof(DamagedVisual));
    }
}
