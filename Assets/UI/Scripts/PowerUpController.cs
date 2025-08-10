using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PowerUpController : MonoBehaviour
{
    public PlayerMove playerMovementScript;
    public PlayerCombat playerCombatScript;
    public NavigationArrow navigationArrow;

    private UIDocument playerHUD;

    private VisualElement doubleJump;
    private VisualElement sprint;
    private VisualElement damage;
    private VisualElement navigation;
    private Label doubleJumpStacks;

    private VisualElement sprintCooldownVisual;
    public float sprintCooldownSeconds = 10f;
    public float sprintCurrentCDTime = 0f;
    private VisualElement damageCooldownVisual;
    public float damageCooldownSeconds = 5f;
    public float damageCurrentCDTime = 0f;
    private VisualElement navigationCooldownVisual;
    public float navigationCooldownSeconds = 5f;
    public float navigationCurrentCDTime = 0f;

    public bool canNavigate = false;

    private void OnEnable()
    {
        playerHUD = GetComponent<UIDocument>();

        doubleJump = playerHUD.rootVisualElement.Q("DoubleJump") as VisualElement;
        sprint = playerHUD.rootVisualElement.Q("Sprint") as VisualElement;
        sprintCooldownVisual = playerHUD.rootVisualElement.Q("Sprint").Q("Cooldown") as VisualElement;
        damage = playerHUD.rootVisualElement.Q("Damage") as VisualElement;
        damageCooldownVisual = playerHUD.rootVisualElement.Q("Damage").Q("Cooldown") as VisualElement;
        navigation = playerHUD.rootVisualElement.Q("Navigation") as VisualElement;
        navigationCooldownVisual = playerHUD.rootVisualElement.Q("Navigation").Q("Cooldown") as VisualElement;
        doubleJumpStacks = playerHUD.rootVisualElement.Q("DoubleJump").Q("Quantity") as Label;

        doubleJump.style.display = DisplayStyle.None;
        sprint.style.display = DisplayStyle.None;
        damage.style.display = DisplayStyle.None;
        navigation.style.display = DisplayStyle.None;
    }

    public void beginSprint()
    {
        StartCoroutine(nameof(SprintCD));
    }

    public void beginDamage()
    {
        StartCoroutine(nameof(DamageCD));
    }

    public void beginNavigation()
    {
        StartCoroutine(nameof(NavigationCD));
        navigation.style.display = DisplayStyle.Flex;
        canNavigate = true;
    }

    public void haveDoubleJump()
    {
        doubleJump.style.display = DisplayStyle.Flex;
    }

    public void haveSprint()
    {
        sprint.style.display = DisplayStyle.Flex;
    }

    public void haveDamage()
    {
        damage.style.display = DisplayStyle.Flex;
    }

    public void usedDamage()
    {
        damage.style.display = DisplayStyle.None;
    }

    public void usedDoubleJump()
    {
        doubleJump.style.display = DisplayStyle.None;
    }

    public void usedSprint()
    {
        sprint.style.display = DisplayStyle.None;
    }

    public void updateDoubleJumpStacks(int quantity)
    {
        doubleJumpStacks.text = quantity.ToString();
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
        sprint.style.display = DisplayStyle.None;
        playerMovementScript.canSprint = false;
        sprintCurrentCDTime = sprintCooldownSeconds;
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
        damage.style.display = DisplayStyle.None;
        playerCombatScript.buffed = false;
        playerCombatScript.endDamageBoost();
        damageCurrentCDTime = damageCooldownSeconds;
    }

    public IEnumerator NavigationCD()
    {
        float elapsedNavigationTime = 0f;
        navigationCurrentCDTime = navigationCooldownSeconds;
        while (elapsedNavigationTime < navigationCurrentCDTime)
        {
            yield return null;
            elapsedNavigationTime += Time.deltaTime;
            navigationCooldownVisual.style.width = (elapsedNavigationTime / navigationCurrentCDTime) * 100;
        }
        navigationArrow.makeArrowInvisible();
        navigation.style.display = DisplayStyle.None;
        navigationCurrentCDTime = navigationCooldownSeconds;
        canNavigate = false;
    }
}
