using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    [SerializeField] GameObject promptPanel;
    [SerializeField] TextMeshProUGUI promptText;

    public void Show(KeyCode key, string actinoText)
    {
        promptPanel.SetActive(true);
        promptText.text = $"[{GetKeyName(key)}] {actinoText}";
    }

    public void Hide()
    {
        promptPanel.SetActive(false);
    }

    private string GetKeyName(KeyCode key)
    {
        if (key == KeyCode.Mouse0)
            return "ÁÂÅ¬¸¯";

        return key.ToString();
    }
}
