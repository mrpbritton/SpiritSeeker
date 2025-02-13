using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            if(player.canDoubleJump == false)
            {
                player.canDoubleJump = true;
                this.gameObject.SetActive(false);
            }
        }
    }
}
