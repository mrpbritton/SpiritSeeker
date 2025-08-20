using UnityEngine;

public class PlayerSwords : MonoBehaviour
{
    public bool canDamage = true;
    public float damage = -1f;
    public bool canDestroyMazeCells = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EnemyHP>(out EnemyHP enemyHP))
        {
            if (canDamage)
            {
                enemyHP.UpdateHealth(damage);
            }
        }
        if(other.CompareTag("MazeCell") && canDestroyMazeCells)
        {
            Destroy(other.gameObject);
        }
    }
}
