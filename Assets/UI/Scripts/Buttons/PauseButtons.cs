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

        resumeButton.RegisterCallback<NavigationSubmitEvent>(ResumeGame);
        restartButton.RegisterCallback<NavigationSubmitEvent>(RestartGame);
        mainMenuButton.RegisterCallback<NavigationSubmitEvent>(LoadMainMenu);
    }
    private void OnDisable()
    {
        controls.Disable();
        resumeButton.UnregisterCallback<NavigationSubmitEvent>(ResumeGame);
        restartButton.UnregisterCallback<NavigationSubmitEvent>(RestartGame);
        mainMenuButton.UnregisterCallback<NavigationSubmitEvent>(LoadMainMenu);
    }

    private void PauseGame(InputAction.CallbackContext ctx)
    {
        pauseDisplay.style.display = DisplayStyle.Flex;
        hud.DeactivateHUD();
        Time.timeScale = 0.0f;

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void ResumeGame(NavigationSubmitEvent evt)
    {
        pauseDisplay.style.display = DisplayStyle.None;
        hud.ReactivateHUD();
        Time.timeScale = 1.0f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
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
        Debug.Log("LoadMainMenu");
        SceneManager.LoadScene(MainMenuScene);
    }

    public void Deactivate()
    {
        this.enabled = false;
    }
}
