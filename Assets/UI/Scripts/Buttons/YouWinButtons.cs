using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class YouWinButtons : MonoBehaviour
{
    public string MainMenuScene;
    [SerializeField] private float parTimeInSeconds = 600;

    private UIDocument youWinDocument;
    private VisualElement youWinDisplay;
    private Button continueButton;
    private Button mainMenuButton;
    private Label score;
    private Label time;
    private Label levelsCleared;

    private void OnEnable()
    {
        youWinDocument = GetComponent<UIDocument>();
        youWinDisplay = youWinDocument.rootVisualElement as VisualElement;

        youWinDisplay.style.display = DisplayStyle.None;

        continueButton = youWinDocument.rootVisualElement.Q("Continue") as Button;
        mainMenuButton = youWinDocument.rootVisualElement.Q("MainMenu") as Button;
        score = youWinDocument.rootVisualElement.Q<Label>("Score");
        time = youWinDocument.rootVisualElement.Q<Label>("Time");   
        levelsCleared = youWinDocument.rootVisualElement.Q<Label>("Levels");

        continueButton.clicked += () => { RestartGame(); };
        mainMenuButton.clicked += () => { LoadMainMenu(); };
    }

    public void Activate()
    {
        youWinDisplay.style.display = DisplayStyle.Flex;

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        // Score Calculations
        ScoreFactors.levelsCleared += 1;
        ScoreFactors.playerScore = ScoreFactors.levelsCleared * ScoreFactors.playerScore;

        // Time Bonus
        if (ScoreFactors.currentTime < parTimeInSeconds)
        {
            ScoreFactors.playerScore += Mathf.RoundToInt((parTimeInSeconds - ScoreFactors.currentTime) * 1000);
        }

        // Update UI
        time.text = "Time: " + ScoreFactors.currentTime.ToString("F2");
        levelsCleared.text = "Levels Cleared: " + ScoreFactors.levelsCleared;
        score.text = "Score: " + ScoreFactors.playerScore;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
    private void LoadMainMenu()
    {
        Debug.Log("LoadMainMenu");
        SceneManager.LoadScene(MainMenuScene);
    }
}
