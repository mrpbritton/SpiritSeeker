using UnityEngine;

public class PlayerSwords : MonoBehaviour
{
    public bool canDamage = true;
    public float damage = -1f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EnemyHP>(out EnemyHP enemyHP))
        {
            if (canDamage)
            {
                enemyHP.UpdateHealth(damage);
            }
        }
    }
}
