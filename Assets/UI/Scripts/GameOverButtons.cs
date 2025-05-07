using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverButtons : MonoBehaviour
{
    public string MainMenuScene;

    private UIDocument gameOverDocument;
    private VisualElement gameOverDisplay;
    private Button restartButton;
    private Button mainMenuButton;

    private void OnEnable()
    {
        gameOverDocument = GetComponent<UIDocument>();
        gameOverDisplay = gameOverDocument.rootVisualElement as VisualElement;

        gameOverDisplay.style.display = DisplayStyle.None;

        restartButton = gameOverDocument.rootVisualElement.Q("Restart") as Button;
        mainMenuButton = gameOverDocument.rootVisualElement.Q("MainMenu") as Button;

        restartButton.RegisterCallback<NavigationSubmitEvent>(RestartGame);
        mainMenuButton.RegisterCallback<NavigationSubmitEvent>(LoadMainMenu);
    }

    private void OnDisable()
    {
        restartButton.UnregisterCallback<NavigationSubmitEvent>(RestartGame);
        mainMenuButton.UnregisterCallback<NavigationSubmitEvent>(LoadMainMenu);
    }

    public void Activate()
    {
        gameOverDisplay.style.display = DisplayStyle.Flex;

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void RestartGame(NavigationSubmitEvent evt)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void LoadMainMenu(NavigationSubmitEvent evt)
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}
