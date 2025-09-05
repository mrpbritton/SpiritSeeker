using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MMButtons : MonoBehaviour
{
    [SerializeField] private Generic_ScreenActivate nextScreen;
    [SerializeField] private UIDocument mainMenu;
    
    private Button startButton;
    private Button exitButton;

    private void OnEnable()
    {
        Time.timeScale = 0;

        startButton = mainMenu.rootVisualElement.Q("StartButton") as Button;
        exitButton = mainMenu.rootVisualElement.Q("ExitButton") as Button;

        startButton.RegisterCallback<NavigationSubmitEvent>(GoToLevelSettings);
        exitButton.RegisterCallback<NavigationSubmitEvent>(ExitGameController);
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<NavigationSubmitEvent>(GoToLevelSettings);
        exitButton.UnregisterCallback<NavigationSubmitEvent>(ExitGameController);
    }

    private void GoToLevelSettings(NavigationSubmitEvent evt)
    {
        nextScreen.ActivateScreen();
        mainMenu.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void ExitGameController(NavigationSubmitEvent evt)
    {
        Debug.Log("Success");
        Application.Quit();
    }
}
