using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] UIDocument playerHUD;
    [SerializeField] string scoreLabelName = "Score";

    private Label scoreLabel;

    private void Start()
    {
        scoreLabel = playerHUD.rootVisualElement.Q<Label>(scoreLabelName);
    }

    void Update()
    {
        scoreLabel.text = "Score: " + ScoreFactors.playerScore.ToString();
    }
}
