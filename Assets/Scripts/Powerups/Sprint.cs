using UnityEngine;

public class Sprint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            if (player.canSprint == false)
            {
                player.canSprint = true;
                this.gameObject.SetActive(false);
            }
        }
    }
}
