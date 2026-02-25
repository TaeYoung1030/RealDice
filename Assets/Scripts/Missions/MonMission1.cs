using UnityEngine;

public class MonMission1 : MonoBehaviour,Missions
{
    [Header("미션 1 전용 UI 데이터")]
    [SerializeField] Sprite Mission1_image;
    [SerializeField] string Mission1_text = "미션 1  : 괴물로 부터 도망가세요";
   public void StartMission()
   {
        UIManager.Instance.ShowUI(Mission1_image, Mission1_text);
        Debug.Log("미션1 진행중");
   }

    //mission 마무리 후 Destroy로 list에서 삭제시키기
}
