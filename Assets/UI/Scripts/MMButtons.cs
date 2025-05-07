using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MMButtons : MonoBehaviour
{
    public string GameScene;

    private UIDocument mainMenu;
    private Button startButton;
    private Button exitButton;

    private void OnEnable()
    {
        Time.timeScale = 0;

        mainMenu = GetComponent<UIDocument>();

        startButton = mainMenu.rootVisualElement.Q("StartButton") as Button;
        exitButton = mainMenu.rootVisualElement.Q("ExitButton") as Button;

        startButton.RegisterCallback<NavigationSubmitEvent>(StartGameController);
        exitButton.RegisterCallback<NavigationSubmitEvent>(ExitGameController);
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<NavigationSubmitEvent>(StartGameController);
        exitButton.UnregisterCallback<NavigationSubmitEvent>(ExitGameController);
    }

    private void StartGameController(NavigationSubmitEvent evt)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(GameScene);
    }

    private void ExitGameController(NavigationSubmitEvent evt)
    {
        Debug.Log("Success");
        Application.Quit();
    }
}
