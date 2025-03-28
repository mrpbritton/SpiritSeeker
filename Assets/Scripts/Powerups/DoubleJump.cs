using UnityEngine;
using UnityEngine.Events;

public class DoubleJump : MonoBehaviour
{
    public UnityEvent doubleJumpActivated;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            doubleJumpActivated.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
