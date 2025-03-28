using UnityEngine;
using UnityEngine.Events;

public class Sprint : MonoBehaviour
{
    public UnityEvent sprintActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            sprintActivated.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
