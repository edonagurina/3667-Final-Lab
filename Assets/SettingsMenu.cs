using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown difficultyDropdown;

    private void Start()
    {
        // Initialize UI from GameManager's current values
        if (GameManager.Instance != null)
        {
            volumeSlider.value = GameManager.Instance.volume;
            difficultyDropdown.value = GameManager.Instance.difficulty;
        }

        // Listeners: update GameManager when UI changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
    }

    public void SetVolume(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetVolume(value);
        }
        else
        {
            // Fallback: at least change audio
            AudioListener.volume = Mathf.Clamp01(value);
        }
    }

    public void SetDifficulty(int diff)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetDifficulty(diff);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
