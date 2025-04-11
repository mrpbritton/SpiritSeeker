using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseButtons : MonoBehaviour
{
    public string MainMenuScene;
    public HUDDeactivate hud;

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
        restartButton = pauseDocument.rootVisualElement.Q("Restart") as Button;
        mainMenuButton = pauseDocument.rootVisualElement.Q("MainMenu") as Button;

        resumeButton.clicked += () => { ResumeGame(); };
        restartButton.clicked += () => { RestartGame(); };
        mainMenuButton.clicked += () => { LoadMainMenu(); };
    }

    private void PauseGame(InputAction.CallbackContext ctx)
    {
        pauseDisplay.style.display = DisplayStyle.Flex;
        hud.DeactivateHUD();
        Time.timeScale = 0.0f;

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void ResumeGame()
    {
        Debug.Log("Resume");
        pauseDisplay.style.display = DisplayStyle.None;
        hud.ReactivateHUD();
        Time.timeScale = 1.0f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
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
    private void OnDisable()
    {
        controls.Disable();
    }

    public void Deactivate()
    {
        this.enabled = false;
    }
}
