using UnityEngine;
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
        mainMenu = GetComponent<UIDocument>();

        startButton = mainMenu.rootVisualElement.Q("StartButton") as Button;
        exitButton = mainMenu.rootVisualElement.Q("ExitButton") as Button;

        startButton.RegisterCallback<ClickEvent>(StartGame);
        exitButton.RegisterCallback<ClickEvent>(ExitGame);
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(StartGame);
        exitButton.UnregisterCallback<ClickEvent>(ExitGame);
    }

    private void StartGame(ClickEvent evt)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(GameScene);
    }

    private void ExitGame(ClickEvent evt)
    {
        Debug.Log("Success");
        Application.Quit();
    }
}
