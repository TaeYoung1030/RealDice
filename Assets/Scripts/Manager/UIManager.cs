using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("미션 UI 뼈대")]
    [SerializeField] GameObject missionPanel;
    [SerializeField] Image missionImage;
    [SerializeField] TextMeshProUGUI missionText;

    [Header("UI 코루틴 시간")]
    [SerializeField] float RoutineTime = 2f;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        missionPanel.SetActive(false);
    }

    public void ShowUI(Sprite MissionImage , string MissionText)
    {
        missionImage.sprite = MissionImage;
        missionText.text = MissionText;

        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        missionPanel.SetActive(true);
        yield return new WaitForSeconds(RoutineTime);
        missionPanel.SetActive(false);
    }
}
