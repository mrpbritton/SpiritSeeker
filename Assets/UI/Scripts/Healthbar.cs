using UnityEngine;
using UnityEngine.UIElements;

public class Healthbar : MonoBehaviour
{
    // UI Doc Related Values
    private UIDocument playerHUD;
    private VisualElement healthbar;
    private float hPBarWidth;
    private bool baseHPFound = false;
    public float HPFill = 1;

    private void OnEnable()
    {
        playerHUD = GetComponent<UIDocument>();
        healthbar = playerHUD.rootVisualElement.Q("HPBar") as VisualElement;
    }

    private void Update()
    {
        // Debug.Log(healthbar.resolvedStyle.width);
        if(healthbar.resolvedStyle.width >= 0 && !baseHPFound)
        {
            baseHPFound = true;
            hPBarWidth = healthbar.resolvedStyle.width;
            Debug.Log(hPBarWidth);
        }
    }

    public void UpdateHPBar(float currentHP, float maxHP)
    {
        if (healthbar.resolvedStyle.width >= 0)
        {
            HPFill = Mathf.Clamp(currentHP / maxHP, 0, 1);
            healthbar.style.width = hPBarWidth * HPFill;
        }
    }
}

