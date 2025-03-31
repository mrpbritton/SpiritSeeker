using UnityEngine;
using UnityEngine.Events;

public class DoubleJump : MonoBehaviour
{
    public UnityEvent doubleJumpActivated;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if(other.TryGetComponent<PlayerMove>(out PlayerMove player))
            {
                if (player.canDoubleJump == false)
                {
                    doubleJumpActivated.Invoke();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
