using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PowerUpController : MonoBehaviour
{
    public PlayerMove playerMovementScript;
    public PlayerCombat playerCombatScript;

    private UIDocument playerHUD;

    private VisualElement doubleJump;
    private VisualElement sprint;
    private VisualElement damage;
    private Label doubleJumpStacks;

    private VisualElement sprintCooldownVisual;
    public float sprintCooldownSeconds = 10f;
    public float sprintCurrentCDTime = 0f;
    private VisualElement damageCooldownVisual;
    public float damageCooldownSeconds = 5f;
    public float damageCurrentCDTime = 0f;

    private void OnEnable()
    {
        playerHUD = GetComponent<UIDocument>();

        doubleJump = playerHUD.rootVisualElement.Q("DoubleJump") as VisualElement;
        sprint = playerHUD.rootVisualElement.Q("Sprint") as VisualElement;
        sprintCooldownVisual = playerHUD.rootVisualElement.Q("Sprint").Q("Cooldown") as VisualElement;
        damage = playerHUD.rootVisualElement.Q("Damage") as VisualElement;
        damageCooldownVisual = playerHUD.rootVisualElement.Q("Damage").Q("Cooldown") as VisualElement;
        doubleJumpStacks = playerHUD.rootVisualElement.Q("DoubleJump").Q("Quantity") as Label;

        doubleJump.style.display = DisplayStyle.None;
        sprint.style.display = DisplayStyle.None;
        damage.style.display = DisplayStyle.None;
    }

    public void beginSprint()
    {
        StartCoroutine(nameof(SprintCD));
    }

    public void beginDamage()
    {
        StartCoroutine(nameof(DamageCD));
    }

    public void haveDoubleJump()
    {
        doubleJump.style.display = DisplayStyle.Flex;
    }

    public void usedDoubleJump()
    {
        doubleJump.style.display = DisplayStyle.None;
    }
    public void haveSprint()
    {
        sprint.style.display = DisplayStyle.Flex;
    }

    public void usedSprint()
    {
        sprint.style.display = DisplayStyle.None;
    }
    public void haveDamage()
    {
        damage.style.display = DisplayStyle.Flex;
    }

    public void usedDamage()
    {
        damage.style.display = DisplayStyle.None;
    }

    public IEnumerator SprintCD()
    {
        float elapsedSprintTime = 0f;
        while (elapsedSprintTime < sprintCurrentCDTime)
        {
            yield return null;
            elapsedSprintTime += Time.deltaTime;
            sprintCooldownVisual.style.width = (elapsedSprintTime / sprintCurrentCDTime) * 100;
        }
        usedSprint();
        playerMovementScript.canSprint = false;
    }
    public IEnumerator DamageCD()
    {
        float elapsedDamageTime = 0f;
        damageCurrentCDTime = damageCooldownSeconds;
        while (elapsedDamageTime < damageCurrentCDTime)
        {
            yield return null;
            elapsedDamageTime += Time.deltaTime;
            damageCooldownVisual.style.width = (elapsedDamageTime / damageCurrentCDTime) * 100;
        }
        usedDamage();
        playerCombatScript.buffed = false;
        playerCombatScript.endDamageBoost();
    }

    public void updateDoubleJumpStacks(int quantity)
    {
        doubleJumpStacks.text = quantity.ToString();
    }
}
