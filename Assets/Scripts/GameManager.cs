using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int MaxLives;
    private int currentLives;

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

        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
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

        if (CurrentScore >= PlayerPrefs.GetInt("HighScore"))
        {
            if (!HighScoreNotice.activeSelf)
            {
                HighScoreNotice.SetActive(true);
            }

            PlayerPrefs.SetInt("HighScore", CurrentScore);

            HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
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
        PlayerPrefs.Save();

        Time.timeScale = 1.0f;

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();

        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}