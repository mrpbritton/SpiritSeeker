using UnityEngine;
using UnityEngine.Events;

public class Victory : MonoBehaviour
{
    public UnityEvent victory;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            victory.Invoke();
            Time.timeScale = 0f;
        }
    }
}
