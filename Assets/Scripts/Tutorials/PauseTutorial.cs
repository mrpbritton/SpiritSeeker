using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseTutorial : MonoBehaviour
{
    public string MainMenuScene;
    public HUDDeactivate hud;

    private UIDocument pauseDocument;

    private VisualElement pauseVE;

    private Button resumeButton;
    private Button restartButton;
    private Button mainMenuButton;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Enable();
        controls.Player.Pause.performed += PauseGame;

        pauseDocument = GetComponent<UIDocument>();
        pauseVE = pauseDocument.rootVisualElement as VisualElement;

        resumeButton = pauseDocument.rootVisualElement.Q("Resume") as Button;
        restartButton = pauseDocument.rootVisualElement.Q("Restart") as Button;
        mainMenuButton = pauseDocument.rootVisualElement.Q("MainMenu") as Button;

        pauseVE.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        resumeButton.RegisterCallback<NavigationSubmitEvent>(ResumeGame);
        restartButton.RegisterCallback<NavigationSubmitEvent>(RestartGame);
        mainMenuButton.RegisterCallback<NavigationSubmitEvent>(LoadMainMenu);
    }

    private void OnDisable()
    {
        resumeButton.UnregisterCallback<NavigationSubmitEvent>(ResumeGame);
        restartButton.UnregisterCallback<NavigationSubmitEvent>(RestartGame);
        mainMenuButton.UnregisterCallback<NavigationSubmitEvent>(LoadMainMenu);
    }

    private void PauseGame(InputAction.CallbackContext ctx)
    {
        pauseVE.style.display = DisplayStyle.Flex;
        Time.timeScale = 0.0f;

        hud.DeactivateHUD();
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void ResumeGame(NavigationSubmitEvent evt)
    {
        pauseVE.style.display = DisplayStyle.None;
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
        SceneManager.LoadScene(MainMenuScene);
        Time.timeScale = 1.0f;
    }
}
