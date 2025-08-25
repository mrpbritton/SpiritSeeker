using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private bool hasFlavorText = false;
    [SerializeField] private bool deactivateOnTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out TutorialTextUpdater player) && other.CompareTag("Player"))
        {
            player.UpdateTutorialText(hasFlavorText);
            if (deactivateOnTrigger)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
