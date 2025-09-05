using UnityEngine;

public class EnemyScoreUpdate : MonoBehaviour
{
    [SerializeField] int scoreValue = 10;

    public void AddScore()
    {
        ScoreFactors.playerScore += scoreValue;
    }
}
