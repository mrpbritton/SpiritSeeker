using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelUpButtons : MonoBehaviour
{
    public HUDDeactivate Hud;
    public float speedIncreaseAmount = 1;
    public float jumpIncreaseAmount = 1;
    public float damageIncreaseAmount = 1;
    public float regenIncreaseAmount = 1;

    private UIDocument levelUpDoc;
    private VisualElement levelUpContainer;
    private Button speed;
    private Button jumpHeight;
    private Button damage;
    private Button health;

    private PlayerCombat combatStats;
    private PlayerMove movementStats;
    private HPController healthStats;

    private void OnEnable()
    {
        levelUpDoc = GetComponent<UIDocument>();
        levelUpContainer = levelUpDoc.rootVisualElement as VisualElement;

        speed = levelUpDoc.rootVisualElement.Q("Speed") as Button;
        jumpHeight = levelUpDoc.rootVisualElement.Q("JumpHeight") as Button;
        damage = levelUpDoc.rootVisualElement.Q("Damage") as Button;
        health = levelUpDoc.rootVisualElement.Q("Regeneration") as Button;

        speed.RegisterCallback<NavigationSubmitEvent>(IncreaseSpeed);
        jumpHeight.RegisterCallback<NavigationSubmitEvent>(IncreaseJumpHeight);
        damage.RegisterCallback<NavigationSubmitEvent>(IncreaseDamage);
        health.RegisterCallback<NavigationSubmitEvent>(IncreaseRegeneration);

        levelUpContainer.style.display = DisplayStyle.None;

        combatStats = GetComponentInParent<PlayerCombat>();
        movementStats = GetComponentInParent<PlayerMove>();
        healthStats = GetComponentInParent<HPController>();
    }

    private void OnDisable()
    {
        speed.UnregisterCallback<NavigationSubmitEvent>(IncreaseSpeed);
        jumpHeight.UnregisterCallback<NavigationSubmitEvent>(IncreaseJumpHeight);
        damage.UnregisterCallback<NavigationSubmitEvent>(IncreaseDamage);
        health.UnregisterCallback<NavigationSubmitEvent>(IncreaseRegeneration);
    }

    public void Activate()
    {
        Hud.DeactivateHUD();

        levelUpContainer.style.display = DisplayStyle.Flex;
    }

    private void IncreaseSpeed(NavigationSubmitEvent evt)
    {
        movementStats.defaultMovementSpeed += speedIncreaseAmount;
        speedIncreaseAmount = speedIncreaseAmount - (speedIncreaseAmount / 5);
        levelUpContainer.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Hud.ReactivateHUD();
    }

    private void IncreaseJumpHeight(NavigationSubmitEvent evt)
    {
        movementStats.jumpHeight += jumpIncreaseAmount;
        jumpIncreaseAmount = jumpIncreaseAmount - (jumpIncreaseAmount / 5);
        levelUpContainer.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Hud.ReactivateHUD();
    }

    private void IncreaseDamage(NavigationSubmitEvent evt)
    {
        foreach (PlayerSwords sword in combatStats.swordArray)
        {
            sword.damage -= damageIncreaseAmount;
        }
        damageIncreaseAmount = damageIncreaseAmount - (damageIncreaseAmount / 5);
        levelUpContainer.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Hud.ReactivateHUD();
    }

    private void IncreaseRegeneration(NavigationSubmitEvent evt)
    {
        healthStats.regenAmount += regenIncreaseAmount;
        regenIncreaseAmount = regenIncreaseAmount - (regenIncreaseAmount / 5);
        levelUpContainer.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Hud.ReactivateHUD();
    }
}
