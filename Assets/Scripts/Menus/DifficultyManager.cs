using UnityEngine;

public static class DifficultyManager
{
    public static float DifficultyMultiplier { get; private set; } = 1f; 
    public static float SpeedMultiplier { get; private set; } = 1f; 

    public static void SetDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                DifficultyMultiplier = 0.5f;
                SpeedMultiplier = 1f;
                break;
            case "Medium":
                DifficultyMultiplier = 1f;
                SpeedMultiplier = 1.2f;
                break;
            case "Hard":
                DifficultyMultiplier = 1.5f;
                SpeedMultiplier = 1.5f;
                break;
            default:
                DifficultyMultiplier = 1f;
                SpeedMultiplier = 1f;
                break;
        }
        Debug.Log($"Difficulty set to {difficulty}, DamageMultiplier: {DifficultyMultiplier} Speed Multiplier: {SpeedMultiplier}");
    }
}
