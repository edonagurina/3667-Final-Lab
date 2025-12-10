using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Awake()
    {
        // Make sure we have the component before Start()
        if (inputField == null)
            inputField = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        if (inputField == null)
        {
            Debug.LogError("❌ PlayerNameInput: InputField is NOT assigned!");
            return;
        }

        // Load saved name if exists
        string savedName = PlayerPrefs.GetString("PlayerName", "");
        inputField.text = savedName;

        // Update GameManager if loaded
        if (GameManager.Instance != null)
            GameManager.Instance.playerName = savedName;

        // Add listener for changes
        inputField.onValueChanged.AddListener(OnNameChanged);
    }

    private void OnNameChanged(string newName)
    {
        PlayerPrefs.SetString("PlayerName", newName);

        if (GameManager.Instance != null)
            GameManager.Instance.playerName = newName;
    }
}
