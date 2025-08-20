using UnityEngine;

public class PlayerEntityTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BasicEnemyMovement>().target = transform;
            other.GetComponent<BasicEnemyMovement>().Activate();
        }
        if(other.CompareTag("PowerUp"))
        {
            other.GetComponent<ActivatePowerUp>().target = transform;
            other.GetComponent<ActivatePowerUp>().Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BasicEnemyMovement>().Deactivate();
        }
        if(other.CompareTag("PowerUp"))
        {
            other.GetComponent<ActivatePowerUp>().Deactivate();
        }
    }
}
