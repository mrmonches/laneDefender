using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int MaxLives;
    private int currentLives;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private int CurrentScore;

    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text LivesText;

    private void Awake()
    {
        currentLives = MaxLives;

        HighScoreText.text = "HighScore: " + _playerStats.PlayerHighScore1;
    }

    public void UpdateScore(int points)
    {
        CurrentScore += points;

        ScoreText.text = "Score: " + CurrentScore;

        if (CurrentScore >= _playerStats.PlayerHighScore1)
        {
            _playerStats.PlayerHighScore1 = CurrentScore;

            HighScoreText.text = "High Score: " + _playerStats.PlayerHighScore1;
        }
    }

    public void UpdateLives()
    {
        currentLives--;

        LivesText.text = "Lives: " + currentLives;

        if (currentLives <= 0)
        {
            print("dead");
        }
    }
}