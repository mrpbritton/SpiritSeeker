using UnityEngine;

public class NavigationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NavigationArrow arrow = other.GetComponentInChildren<NavigationArrow>();
            if (arrow != null)
            {

            }
        }
    }
}
