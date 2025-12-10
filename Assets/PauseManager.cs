using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToTitle()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.TryAddHighScore();

        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
