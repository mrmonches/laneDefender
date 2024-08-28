using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int MaxLives;
    private int currentLives;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private int CurrentScore;

    public void UpdateScore(int points)
    {
        CurrentScore += points;

        if (CurrentScore >= _playerStats.PlayerHighScore1)
        {
            _playerStats.PlayerHighScore1 = CurrentScore;
        }
    }

    public void UpdateLives()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            print("dead");
        }
    }
}

