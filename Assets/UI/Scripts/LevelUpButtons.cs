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

        speed.RegisterCallback<ClickEvent>(IncreaseSpeed);
        jumpHeight.RegisterCallback<ClickEvent>(IncreaseJumpHeight);
        damage.RegisterCallback<ClickEvent>(IncreaseDamage);
        health.RegisterCallback<ClickEvent>(IncreaseRegeneration);

        levelUpContainer.style.display = DisplayStyle.None;

        combatStats = GetComponentInParent<PlayerCombat>();
        movementStats = GetComponentInParent<PlayerMove>();
        healthStats = GetComponentInParent<HPController>();
    }

    private void OnDisable()
    {
        speed.UnregisterCallback<ClickEvent>(IncreaseSpeed);
        jumpHeight.UnregisterCallback<ClickEvent>(IncreaseJumpHeight);
        damage.UnregisterCallback<ClickEvent>(IncreaseDamage);
        health.UnregisterCallback<ClickEvent>(IncreaseRegeneration);
    }

    public void Activate()
    {
        Hud.DeactivateHUD();

        levelUpContainer.style.display = DisplayStyle.Flex;
    }

    private void IncreaseSpeed(ClickEvent evt)
    {
        movementStats.defaultMovementSpeed += speedIncreaseAmount;
        speedIncreaseAmount = speedIncreaseAmount - (speedIncreaseAmount / 5);
        levelUpContainer.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Hud.ReactivateHUD();
    }

    private void IncreaseJumpHeight(ClickEvent evt)
    {
        movementStats.jumpHeight += jumpIncreaseAmount;
        jumpIncreaseAmount = jumpIncreaseAmount - (jumpIncreaseAmount / 5);
        levelUpContainer.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Hud.ReactivateHUD();
    }

    private void IncreaseDamage(ClickEvent evt)
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

    private void IncreaseRegeneration(ClickEvent evt)
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
