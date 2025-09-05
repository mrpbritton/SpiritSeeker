using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerUpdater : MonoBehaviour
{
    [SerializeField] UIDocument playerHUD;
    [SerializeField] string timerLabelName = "Timer";

    private Label timerLabel;
    private float timeElapsed = 0; 

    private void Start()
    {
        timerLabel = playerHUD.rootVisualElement.Q<Label>(timerLabelName);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        timerLabel.text = "Time: " + timeElapsed.ToString("F2");
        ScoreFactors.currentTime = timeElapsed;
    }
}