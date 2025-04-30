using UnityEngine;
using UnityEngine.UIElements;

public class XPBar : MonoBehaviour
{
    // Level Up Screen Stuff
    public LevelUpButtons levelUpScreen;

    // UI Doc Related Values
    private UIDocument playerHUD;
    private VisualElement experienceBar;
    private HUDDeactivate completeHUD;
    private float xPBarWidth;
    private bool baseXPFound = false;
    public float XPFill = 1;

    private float currentXp = 0;
    public float maxXp = 100;

    private void OnEnable()
    {
        playerHUD = GetComponent<UIDocument>();
        experienceBar = playerHUD.rootVisualElement.Q("XPFill") as VisualElement;
        completeHUD = GetComponent<HUDDeactivate>();
    }

    private void Update()
    {
        // Set up the XP Bar at the start
        if (experienceBar.resolvedStyle.width >= 0 && !baseXPFound)
        {
            baseXPFound = true;
            xPBarWidth = experienceBar.resolvedStyle.width;
            UpdateXPBar(0, maxXp);
        }

        // Level Up
        if (currentXp >= maxXp)
        {
            currentXp = currentXp - maxXp;
            UpdateXPBar(currentXp, maxXp);
            completeHUD.DeactivateHUD();
            levelUpScreen.Activate();
            Time.timeScale = 0;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            maxXp = maxXp * 1.75f;
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
