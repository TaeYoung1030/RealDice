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

    [Header("미션 고정위치 UI")]
    [SerializeField] GameObject Panel;
    [SerializeField] TextMeshProUGUI panelText;

    [Header("UI 코루틴 시간")]
    [SerializeField] float RoutineTime = 2f;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        missionPanel.SetActive(false);
        Panel.SetActive(false);
    }

    public void ShowUI(Sprite MissionImage , string MissionText)
    {
        missionImage.sprite = MissionImage;
        missionText.text = MissionText;

        panelText.text = MissionText;

        StartCoroutine(ShowRoutine());
        //미션을 완료하거나 다음 미션으로 넘어가면 지금 미션을 다시 false로 바꾸는 방법 생각해보기
    }

    IEnumerator ShowRoutine()
    {
        missionPanel.SetActive(true);
        yield return new WaitForSeconds(RoutineTime);
        missionPanel.SetActive(false);
        Panel.SetActive(true);
    }
}
