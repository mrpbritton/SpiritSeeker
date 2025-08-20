using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PowerUpController : MonoBehaviour
{
    // Player Related Scripts
    public PlayerMove playerMovementScript;
    public PlayerCombat playerCombatScript;
    public NavigationArrow navigationArrow;

    private UIDocument playerHUD;

    // Double Jump Stuff
    private VisualElement doubleJump;
    private Label doubleJumpStacks;

    // Sprint Stuff
    private VisualElement sprint;
    private VisualElement sprintCooldownVisual;
    public float sprintCooldownSeconds = 10f;
    public float sprintCurrentCDTime = 0f;

    // Damage Stuff
    private VisualElement damage;
    private VisualElement damageCooldownVisual;
    public float damageCooldownSeconds = 5f;
    public float damageCurrentCDTime = 0f;

    // Navigation Stuff
    private VisualElement navigation;
    private VisualElement navigationCooldownVisual;
    public float navigationCooldownSeconds = 5f;
    public float navigationCurrentCDTime = 0f;
    public bool canNavigate = false;

    // Wall Destruction Stuff
    private VisualElement wallDestruction;
    private VisualElement wallDestructionCooldownVisual;
    public float wallDestructionCooldownSeconds = 5f;
    public float wallDestructionCurrentCDTime = 0f;
    public bool canDestroyWalls = false;

    // Floats That Exist Because Unity UI Toolkit is Stupid
    private float sprintCooldownWidth = 0f;
    private float damageCooldownWidth = 0f;
    private float navigationCooldownWidth = 0f;
    private float wallDestructionCooldownWidth = 0f;

    // Bools That Exist Because Unity UI Toolkit is Stupid
    private bool sprintWidthHasBeenSet = false;
    private bool damageWidthHasBeenSet = false;
    private bool navigationWidthHasBeenSet = false;
    private bool wallDestructionWidthHasBeenSet = false;

    private void OnEnable()
    {
        playerHUD = GetComponent<UIDocument>();

        // Get Double Jump UI Elements and Hide
        doubleJump = playerHUD.rootVisualElement.Q("DoubleJump") as VisualElement;
        doubleJumpStacks = playerHUD.rootVisualElement.Q("DoubleJump").Q("Quantity") as Label;
        doubleJump.style.display = DisplayStyle.None;

        // Get Sprint UI Elements and Hide
        sprint = playerHUD.rootVisualElement.Q("Sprint") as VisualElement;
        sprintCooldownVisual = playerHUD.rootVisualElement.Q("Sprint").Q("Cooldown") as VisualElement;
        sprint.style.display = DisplayStyle.None;

        // Get Damage UI Elements and Hide
        damage = playerHUD.rootVisualElement.Q("Damage") as VisualElement;
        damageCooldownVisual = playerHUD.rootVisualElement.Q("Damage").Q("Cooldown") as VisualElement;
        damage.style.display = DisplayStyle.None;

        // Get Navigation UI Elements and Hide
        navigation = playerHUD.rootVisualElement.Q("Navigation") as VisualElement;
        navigationCooldownVisual = playerHUD.rootVisualElement.Q("Navigation").Q("Cooldown") as VisualElement;
        navigation.style.display = DisplayStyle.None;

        // Get Wall Destruction UI Elements and Hide
        wallDestruction = playerHUD.rootVisualElement.Q("WallDestruction") as VisualElement;
        wallDestructionCooldownVisual = playerHUD.rootVisualElement.Q("WallDestruction").Q("Cooldown") as VisualElement;
        wallDestruction.style.display = DisplayStyle.None;

        // Register Callbacks That Exist Because Unity UI Toolkit is Stupid
        sprintCooldownVisual.RegisterCallback<GeometryChangedEvent>(OnSprintGeometryChanged);
        damageCooldownVisual.RegisterCallback<GeometryChangedEvent>(OnDamageGeometryChanged);
        navigationCooldownVisual.RegisterCallback<GeometryChangedEvent>(OnNavigationGeometryChanged);
        wallDestructionCooldownVisual.RegisterCallback<GeometryChangedEvent>(OnWallDestructionGeometryChanged);
    }

    // Functions That Exist Because Unity UI Toolkit is Stupid
    void OnSprintGeometryChanged(GeometryChangedEvent evt)
    {
        if(!sprintWidthHasBeenSet)
        {
            sprintCooldownWidth = sprintCooldownVisual.resolvedStyle.width;
            sprintWidthHasBeenSet = true;
        }
    }

    void OnDamageGeometryChanged(GeometryChangedEvent evt)
    {
        if(!damageWidthHasBeenSet)
        {
            damageCooldownWidth = damageCooldownVisual.resolvedStyle.width;
            damageWidthHasBeenSet = true;
        }
    }

    void OnNavigationGeometryChanged(GeometryChangedEvent evt)
    {
        if(!navigationWidthHasBeenSet)
        {
            navigationCooldownWidth = navigationCooldownVisual.resolvedStyle.width;
            navigationWidthHasBeenSet = true;
        }
    }

    void OnWallDestructionGeometryChanged(GeometryChangedEvent evt)
    {
        if(!wallDestructionWidthHasBeenSet)
        {
            wallDestructionCooldownWidth = wallDestructionCooldownVisual.resolvedStyle.width;
            wallDestructionWidthHasBeenSet = true;
        }
    }

    // Power Up Acquired Functions
    public void haveSprint()
    {
        sprint.style.display = DisplayStyle.Flex;
    }

    public void beginSprint()
    {
        StartCoroutine(nameof(SprintCD));
    }

    public void beginDamage()
    {
        StartCoroutine(nameof(DamageCD));
        damage.style.display = DisplayStyle.Flex;
    }

    public void beginNavigation()
    {
        StartCoroutine(nameof(NavigationCD));
        navigation.style.display = DisplayStyle.Flex;
        canNavigate = true;
    }

    public void beginWallDestruction()
    {
        StartCoroutine(nameof(WallDestructionCD));
        wallDestruction.style.display = DisplayStyle.Flex;
        canDestroyWalls = true;
    }

    public void acquiredDoubleJump()
    {
        doubleJump.style.display = DisplayStyle.Flex;
    }


    // Power Up Consumed Functions
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


    // Power Up Cooldown Coroutines
    public IEnumerator SprintCD()
    {
        float elapsedSprintTime = 0f;
        sprintCurrentCDTime = sprintCooldownSeconds;

        while (elapsedSprintTime < sprintCurrentCDTime)
        {
            yield return null;
            if (playerMovementScript.isSprinting)
            {
                elapsedSprintTime += Time.deltaTime;
            }
            sprintCooldownVisual.style.width = (elapsedSprintTime / sprintCurrentCDTime) * sprintCooldownWidth;
        }

        sprint.style.display = DisplayStyle.None;
        playerMovementScript.canSprint = false;
        sprintCurrentCDTime = sprintCooldownSeconds;
        StopCoroutine(nameof(SprintCD));
    }

    public IEnumerator DamageCD()
    {
        float elapsedDamageTime = 0f;
        damageCurrentCDTime = damageCooldownSeconds;

        while (elapsedDamageTime < damageCurrentCDTime)
        {
            yield return null;
            elapsedDamageTime += Time.deltaTime;
            damageCooldownVisual.style.width = (elapsedDamageTime / damageCurrentCDTime) * damageCooldownWidth;
        }
        damage.style.display = DisplayStyle.None;
        playerCombatScript.buffed = false;
        playerCombatScript.endDamageBoost();
        damageCurrentCDTime = damageCooldownSeconds;
        StopCoroutine(nameof(DamageCD));
    }

    public IEnumerator NavigationCD()
    {
        float elapsedNavigationTime = 0f;
        navigationCurrentCDTime = navigationCooldownSeconds;
        while (elapsedNavigationTime < navigationCurrentCDTime)
        {
            yield return null;
            elapsedNavigationTime += Time.deltaTime;
            navigationCooldownVisual.style.width = (elapsedNavigationTime / navigationCurrentCDTime) * navigationCooldownWidth;
        }
        navigationArrow.makeArrowInvisible();
        navigation.style.display = DisplayStyle.None;
        navigationCurrentCDTime = navigationCooldownSeconds;
        canNavigate = false;
        StopCoroutine(nameof(NavigationCD));
    }

    public IEnumerator WallDestructionCD()
    {
        float elapsedWallDestructionTime = 0f;
        wallDestructionCurrentCDTime = wallDestructionCooldownSeconds;
        playerCombatScript.EnableMazeDestruction();
        while (elapsedWallDestructionTime < wallDestructionCurrentCDTime)
        {
            yield return null;
            elapsedWallDestructionTime += Time.deltaTime;
            wallDestructionCooldownVisual.style.width = (elapsedWallDestructionTime / wallDestructionCurrentCDTime) * wallDestructionCooldownWidth;
        }
        wallDestruction.style.display = DisplayStyle.None;
        wallDestructionCurrentCDTime = wallDestructionCooldownSeconds;
        playerCombatScript.DisableMazeDestruction();
        canDestroyWalls = false;
        StopCoroutine(nameof(WallDestructionCD));
    }
}
