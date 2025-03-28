using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpController : MonoBehaviour
{
    private UIDocument playerHUD;

    private VisualElement doubleJump;
    private VisualElement sprint;
    private VisualElement damage;

    private void OnEnable()
    {
        playerHUD = GetComponent<UIDocument>();

        doubleJump = playerHUD.rootVisualElement.Q("DoubleJump") as VisualElement;
        sprint = playerHUD.rootVisualElement.Q("Sprint") as VisualElement;
        damage = playerHUD.rootVisualElement.Q("Damage") as VisualElement;

        doubleJump.style.display = DisplayStyle.None;
        sprint.style.display = DisplayStyle.None;
        damage.style.display = DisplayStyle.None;
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
}
