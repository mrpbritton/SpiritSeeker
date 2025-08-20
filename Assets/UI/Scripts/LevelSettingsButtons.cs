using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSettingsButtons : MonoBehaviour
{
    [SerializeField] private UIDocument levelSettingsDocument;
    [SerializeField] private Generic_ScreenActivate backScreen;
    [SerializeField] private string levelName;

    private Label errorMessage;
    private Button startButton;
    private Button backButton;
    private DropdownField mazeSize;
    private DropdownField enemyQuantity;

    private void OnEnable()
    {
        Time.timeScale = 0;

        errorMessage = levelSettingsDocument.rootVisualElement.Q<Label>("ErrorMessage");
        errorMessage.text = "<color=black>Please select the kind of adventure you'd like to have.</color>";

        startButton = levelSettingsDocument.rootVisualElement.Q("Start") as Button;
        backButton = levelSettingsDocument.rootVisualElement.Q("Back") as Button;
        mazeSize = levelSettingsDocument.rootVisualElement.Q<DropdownField>("Size");
        enemyQuantity = levelSettingsDocument.rootVisualElement.Q<DropdownField>("Enemies");

        startButton.RegisterCallback<NavigationSubmitEvent>(StartGameController);
        backButton.RegisterCallback<NavigationSubmitEvent>(BackToMainMenu);
        
        levelSettingsDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<NavigationSubmitEvent>(StartGameController);
        backButton.UnregisterCallback<NavigationSubmitEvent>(BackToMainMenu);
    }

    private void StartGameController(NavigationSubmitEvent evt)
    {
        if (mazeSize.value == null || enemyQuantity.value == null)
        {
            errorMessage.text = "<color=red>Please select the kind of adventure you'd like to have.</color>";
            return;
        }
        Time.timeScale = 1;
        DataManager.mazeSize = mazeSize.value switch
        {
            "Small" => 1,
            "Medium" => 2,
            "Large" => 3,
            _ => 1 // Default
        };
        DataManager.enemySpawnRate = enemyQuantity.value switch
        {
            "A Few" => 1,
            "A Great Many" => 2,
            "Countless" => 3,
            _ => 1 // Default
        };
        SceneManager.LoadScene(levelName);
    }

    private void BackToMainMenu(NavigationSubmitEvent evt)
    {
        backScreen.ActivateScreen();
        levelSettingsDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
}
