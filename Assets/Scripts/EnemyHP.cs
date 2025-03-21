using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 10;
    public Material lowHealthMaterial;

    private Renderer mRenderer;
    private float currentHP;
    private bool canTakeDamage = true;
    private float damageCD = 1f;

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
            if(currentHP <= 2)
            {
                mRenderer.material = lowHealthMaterial;
            }
            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
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
