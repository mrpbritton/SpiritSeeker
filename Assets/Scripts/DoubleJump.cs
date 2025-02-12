using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            Debug.Log("Power Up");
            player.canDoubleJump = true;
            this.gameObject.SetActive(false);
        }
    }
}
