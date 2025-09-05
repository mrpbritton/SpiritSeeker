using UnityEngine;

public static class ScoreFactors
{
    public static int playerScore = 0;
    public static int levelsCleared = 0;
    public static float currentTime = 0f;

    public static void Reset()
    {
        playerScore = 0;
        levelsCleared = 0;
        currentTime = 0f;
    }
}
