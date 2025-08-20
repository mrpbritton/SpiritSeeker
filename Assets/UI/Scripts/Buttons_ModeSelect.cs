using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class Buttons_ModeSelect : MonoBehaviour
{
    [SerializeField] private Generic_ScreenActivate endlessModeScreen;
    [SerializeField] private Generic_ScreenActivate mainMenuScreen;
    [SerializeField] private UIDocument modeSelectDocument;
    [SerializeField] private string tutorialSceneName;

    private Button tutorialButton;
    private Button storyModeButton;
    private Button endlessModeButton;
    private Button backButton;
    private Label storyModeError;

    private void OnEnable()
    {
        Time.timeScale = 0;

        tutorialButton = modeSelectDocument.rootVisualElement.Q("Tutorial") as Button;
        storyModeButton = modeSelectDocument.rootVisualElement.Q("Story") as Button;
        endlessModeButton = modeSelectDocument.rootVisualElement.Q("Endless") as Button;
        backButton = modeSelectDocument.rootVisualElement.Q("Back") as Button;
        Debug.Log(backButton);

        storyModeError = modeSelectDocument.rootVisualElement.Q<Label>("ErrorMessage");
        if (storyModeError != null)
        {
            storyModeError.text = "<color=black>This feature has not yet been implemented yet.</color>";
        }

        tutorialButton.RegisterCallback<NavigationSubmitEvent>(enterTutorial);
        storyModeButton.RegisterCallback<NavigationSubmitEvent>(startStoryMode);
        endlessModeButton.RegisterCallback<NavigationSubmitEvent>(startEndlessMode);
        backButton.RegisterCallback<NavigationSubmitEvent>(backToMainMenu);
    }

    private void OnDisable()
    {
        tutorialButton.UnregisterCallback<NavigationSubmitEvent>(enterTutorial);
        storyModeButton.UnregisterCallback<NavigationSubmitEvent>(startStoryMode);
        endlessModeButton.UnregisterCallback<NavigationSubmitEvent>(startEndlessMode);
        backButton.UnregisterCallback<NavigationSubmitEvent>(backToMainMenu);
    }

    private void enterTutorial(NavigationSubmitEvent evt)
    {
        SceneManager.LoadScene(tutorialSceneName);
    }

    private void startStoryMode(NavigationSubmitEvent evt)
    {
        if (storyModeError != null)
        {
            storyModeError.text = "<color=red>This feature has not yet been implemented yet.</color>";
        }
    }

    private void startEndlessMode(NavigationSubmitEvent evt)
    {
        endlessModeScreen.ActivateScreen();
        modeSelectDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
    private void backToMainMenu(NavigationSubmitEvent evt)
    {
        mainMenuScreen.ActivateScreen();
        modeSelectDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
}
