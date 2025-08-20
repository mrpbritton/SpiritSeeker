using UnityEngine;
using UnityEngine.UIElements;

public class Generic_ScreenActivate : MonoBehaviour
{
    [SerializeField] private UIDocument thisDocument;
    private VisualElement screenVisuals;

    private void Start()
    {
        screenVisuals = thisDocument.rootVisualElement;
    }

    public void ActivateScreen()
    {
        if (screenVisuals != null)
        {
            screenVisuals.style.display = DisplayStyle.Flex;
        }
    }
}
