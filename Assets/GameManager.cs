using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;

    [Header("Player Data")]
    public int score = 0;
    public string playerName = "Player1";

    [Header("Settings")]
    public float volume = 1f;
    public int difficulty = 1;

    // High Score System
    public List<int> highScores = new List<int>();

    private const string HighScoreKey = "HighScores";
    private const string VolumeKey = "Volume";
    private const string DifficultyKey = "Difficulty";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load settings and scores once when the game starts
            LoadSettings();
            LoadHighScores();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSettings();

        GameObject found = GameObject.FindWithTag("ScoreText");
        if (found != null)
            scoreText = found.GetComponent<TextMeshProUGUI>();

        // Reset score ONLY on Title
        if (scene.buildIndex == 0)
        {
            score = 0;
        }

        UpdateScoreText();
    }

    public void AddScore(float balloonScale)
    {
        int points = Mathf.RoundToInt(10f / balloonScale);
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void SetVolume(float v)
    {
        volume = Mathf.Clamp01(v);
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetDifficulty(int d)
    {
        difficulty = d;

        PlayerPrefs.SetInt(DifficultyKey, difficulty);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        difficulty = PlayerPrefs.GetInt(DifficultyKey, 1);

        AudioListener.volume = volume;
    }

    public void TryAddHighScore()
    {
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a));

        if (highScores.Count > 5)
            highScores.RemoveRange(5, highScores.Count - 5);

        SaveHighScores();
    }

    private void SaveHighScores()
    {
        string joined = string.Join(",", highScores);
        PlayerPrefs.SetString(HighScoreKey, joined);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        highScores.Clear();

        if (!PlayerPrefs.HasKey(HighScoreKey))
            return;

        string saved = PlayerPrefs.GetString(HighScoreKey);
        string[] parts = saved.Split(',');

        foreach (var p in parts)
        {
            if (int.TryParse(p, out int value))
                highScores.Add(value);
        }
    }

    public void RestartLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            TryAddHighScore();
            SceneManager.LoadScene(0);
        }
    }

    public void LoadNextLevelWithDelay(float delay)
    {
        StartCoroutine(LoadNextSceneDelayed(delay));
    }

    private System.Collections.IEnumerator LoadNextSceneDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextLevel();
    }
}
