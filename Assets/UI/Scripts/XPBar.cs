using UnityEngine;
using UnityEngine.UIElements;

public class XPBar : MonoBehaviour
{
    // UI Doc Related Values
    private UIDocument playerHUD;
    private VisualElement experienceBar;
    private float xPBarWidth;
    private bool baseXPFound = false;
    public float XPFill = 1;

    private float currentXp = 0;
    public float maxXp = 100;

    private void OnEnable()
    {
        playerHUD = GetComponent<UIDocument>();
        experienceBar = playerHUD.rootVisualElement.Q("XPFill") as VisualElement;
    }

    private void Update()
    {
        if (experienceBar.resolvedStyle.width >= 0 && !baseXPFound)
        {
            baseXPFound = true;
            xPBarWidth = experienceBar.resolvedStyle.width;
            UpdateXPBar(0, maxXp);
        }

        if (currentXp >= maxXp)
        {
            UpdateXPBar(0, maxXp);
            currentXp = 0;
        }
    }

    public void XPGained(float amount)
    {
        currentXp += amount;
        UpdateXPBar(currentXp, maxXp);
    }

    public void UpdateXPBar(float currentXP, float maxXP)
    {
        if (experienceBar.resolvedStyle.width >= 0)
        {
            XPFill = Mathf.Clamp(currentXP / maxXP, 0, 1);
            experienceBar.style.width = xPBarWidth * XPFill;
        }
    }
}
