using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int MaxLives;
    private int currentLives;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private int CurrentScore;

    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text LivesText;

    [SerializeField] private GameObject LoseScreenObject;
    [SerializeField] private GameObject HighScoreNotice;

    [SerializeField] private AudioSource MusicSource;

    private void Awake()
    {
        currentLives = MaxLives;

        HighScoreText.text = "HighScore: " + _playerStats.PlayerHighScore1;
    }

    /// <summary>
    /// Updates score under the hood, as well as on the screen
    /// Score for current game and high score
    /// </summary>
    /// <param name="points"></param>
    public void UpdateScore(int points)
    {
        CurrentScore += points;

        ScoreText.text = "Score: " + CurrentScore;

        if (CurrentScore >= _playerStats.PlayerHighScore1)
        {
            if (!HighScoreNotice.activeSelf)
            {
                HighScoreNotice.SetActive(true);
            }

            _playerStats.PlayerHighScore1 = CurrentScore;

            HighScoreText.text = "High Score: " + _playerStats.PlayerHighScore1;
        }
    }

    /// <summary>
    /// Updates lives by taking away from current count
    /// </summary>
    public void UpdateLives()
    {
        currentLives--;

        LivesText.text = "Lives: " + currentLives;

        if (currentLives <= 0)
        {
            LoseScreenObject.SetActive(true);

            MusicSource.Stop();

            Time.timeScale = 0.0f;
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}