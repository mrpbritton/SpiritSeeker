using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class YouWinButtons : MonoBehaviour
{
    public string MainMenuScene;

    private UIDocument youWinDocument;
    private VisualElement youWinDisplay;
    private Button restartButton;
    private Button mainMenuButton;

    private void OnEnable()
    {
        youWinDocument = GetComponent<UIDocument>();
        youWinDisplay = youWinDocument.rootVisualElement as VisualElement;

        youWinDisplay.style.display = DisplayStyle.None;

        restartButton = youWinDocument.rootVisualElement.Q("Restart") as Button;
        mainMenuButton = youWinDocument.rootVisualElement.Q("MainMenu") as Button;

        restartButton.clicked += () => { RestartGame(); };
        mainMenuButton.clicked += () => { LoadMainMenu(); };
    }

    public void Activate()
    {
        youWinDisplay.style.display = DisplayStyle.Flex;

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
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
