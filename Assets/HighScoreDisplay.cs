using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        if (GameManager.Instance == null) return;

        var list = GameManager.Instance.highScores;

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int value = (i < list.Count) ? list[i] : 0;
            scoreTexts[i].text = $"{i + 1}. {value}";
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Title");
    }
}

