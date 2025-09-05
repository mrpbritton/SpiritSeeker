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
    private Label score;
    private Label levelsCleared;


    private void OnEnable()
    {
        gameOverDocument = GetComponent<UIDocument>();
        gameOverDisplay = gameOverDocument.rootVisualElement as VisualElement;

        gameOverDisplay.style.display = DisplayStyle.None;

        restartButton = gameOverDocument.rootVisualElement.Q("Restart") as Button;
        mainMenuButton = gameOverDocument.rootVisualElement.Q("MainMenu") as Button;
        score = gameOverDocument.rootVisualElement.Q<Label>("Score");
        levelsCleared = gameOverDocument.rootVisualElement.Q<Label>("Levels");

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

        // Update UI
        levelsCleared.text = "Levels Cleared: " + ScoreFactors.levelsCleared;
        score.text = "Score: " + ScoreFactors.playerScore;
    }

    private void RestartGame(NavigationSubmitEvent evt)
    {
        ScoreFactors.Reset();
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
