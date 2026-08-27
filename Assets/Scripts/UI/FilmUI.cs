using TMPro;
using UnityEngine;

public class FilmUI : MonoBehaviour
{
    [SerializeField] GameObject filmPanel;
    [SerializeField] TextMeshProUGUI filmText;

    private void Start()
    {
        //filmPanel.SetActive(PlayerInventory.Instance.HasCamera);

        PlayerInventory.Instance.CameraChanged += SetVisible;
        PlayerInventory.Instance.FilmChanged += UpdateText;

        SetVisible(PlayerInventory.Instance.HasCamera);
        UpdateText(PlayerInventory.Instance.FilmCount);
    }

    void Show()
    {
        filmPanel.SetActive(true);
    }

    void UpdateText(int count)
    {
        filmText.text = $"현재 필름 개수 : {count}개";
        //현재 필름 개수 : x개 로 틀 맞추기 
    }

    private void SetVisible(bool hasCamera)
    {
        filmPanel.SetActive(hasCamera);
    }
}
