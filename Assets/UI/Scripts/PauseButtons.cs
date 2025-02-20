using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseButtons : MonoBehaviour
{
    public string MainMenuScene;

    private UIDocument pauseDocument;
    private VisualElement pauseDisplay;
    private Controls controls;
    private Button resumeButton;
    private Button restartButton;
    private Button mainMenuButton;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Pause.performed += PauseGame;

        pauseDocument = GetComponent<UIDocument>();
        pauseDisplay = pauseDocument.rootVisualElement as VisualElement;

        pauseDisplay.style.display = DisplayStyle.None;

        resumeButton = pauseDocument.rootVisualElement.Q("Resume") as Button;
        // restartButton = pauseDocument.rootVisualElement.Q("Restart") as Button;
        mainMenuButton = pauseDocument.rootVisualElement.Q("MainMenu") as Button;

        resumeButton.RegisterCallback<ClickEvent>(ResumeGame);
        // restartButton.RegisterCallback<ClickEvent>(RestartGame);
        mainMenuButton.RegisterCallback<ClickEvent>(LoadMainMenu);
    }

    private void PauseGame(InputAction.CallbackContext ctx)
    {
        pauseDisplay.style.display = DisplayStyle.Flex;
        Time.timeScale = 0.0f;
    }

    private void ResumeGame(ClickEvent evt)
    {
        Debug.Log("Resume");
        // pauseDisplay.style.display = DisplayStyle.None;
        // Time.timeScale = 1.0f;
    }
    private void RestartGame(ClickEvent evt)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void LoadMainMenu(ClickEvent evt)
    {
        Debug.Log("LoadMainMenu");
        SceneManager.LoadScene(MainMenuScene);
    }
    private void OnDisable()
    {
        controls.Disable();
    }

}
