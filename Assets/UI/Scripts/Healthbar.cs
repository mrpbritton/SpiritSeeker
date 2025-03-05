using UnityEngine;
using UnityEngine.UIElements;

public class Healthbar : MonoBehaviour
{
    [Range(0, 1)] public float HPFill = 1;

    private UIDocument playerHUD;
    private VisualElement healthbar;
    private float baseHP;
    private bool baseHPFound = false;

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
            baseHP = healthbar.resolvedStyle.width;
            Debug.Log(baseHP);
        }
        if(healthbar.resolvedStyle.width >= 0)
        {
            healthbar.style.width = baseHP * HPFill;
        }
    }
}

