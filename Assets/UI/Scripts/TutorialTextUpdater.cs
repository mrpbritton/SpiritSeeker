using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialTextUpdater : MonoBehaviour
{
    [SerializeField] private UIDocument tutorialHUD;
    [SerializeField] private float textUpdateInterval = 5f;
    [SerializeField] private string tutorialLabelName = "TutorialText";
    [SerializeField] private string[] tutorialTextLines;

    private Label tutorialTextLabel;
    private int lineIndex = 0;

    private void OnEnable()
    {
        tutorialTextLabel = tutorialHUD.rootVisualElement.Q<Label>(tutorialLabelName);
        StartCoroutine(flavorTextCooldown());
    }

    public void UpdateTutorialText(bool hasFlavorText)
    {
        changeText();
        if (hasFlavorText)
        {
            StartCoroutine(flavorTextCooldown());
        }
    }

    private void changeText()
    {
        if (lineIndex != tutorialTextLines.Length - 1)
        {
            lineIndex++;
            tutorialTextLabel.text = tutorialTextLines[lineIndex];
        }
    }

    private IEnumerator flavorTextCooldown()
    {
        yield return new WaitForSeconds(textUpdateInterval);
        changeText();
        StopCoroutine(nameof(flavorTextCooldown));
    }
}
