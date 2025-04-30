using UnityEngine;
using UnityEngine.UIElements;

public class HUDDeactivate : MonoBehaviour
{
    private UIDocument hudDoc;
    private VisualElement hudDisplay;

    private void OnEnable()
    {
        hudDoc = GetComponent<UIDocument>();
        hudDisplay = hudDoc.rootVisualElement as VisualElement;
    }

    public void DeactivateHUD()
    {
        hudDisplay.style.display = DisplayStyle.None;
    }

    public void ReactivateHUD()
    {
        hudDisplay.style.display = DisplayStyle.Flex;
    }
}
