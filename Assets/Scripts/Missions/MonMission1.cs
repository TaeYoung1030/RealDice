using System;
using UnityEngine;

public class MonMission1 : MonoBehaviour,Missions
{
    [Header("미션 1 전용 UI 데이터")]
    [SerializeField] Sprite Mission1_image;
    [SerializeField] string Mission1_text = "미션 1  : 괴물로 부터 도망가세요";

    [Header("해당 몬스터 소환")]
    [SerializeField] EnemyStateManager esm;
    [SerializeField] Transform spawnPoint;

    //public static MonMission1 Instance;

    //public event Action<String> Changed;

    private void Awake()
    {
        //Instance = this;
    }
    public void StartMission()
   {
        UIManager.Instance.ShowUI(Mission1_image, Mission1_text);

        EnemyStateManager spawnedMon = Instantiate(esm, spawnPoint.position, spawnPoint.rotation);
        //해당 event 구독
        spawnedMon.MonsterDie += EndMission;
        
        Debug.Log("미션1 진행중");
   }

    private void EndMission()
    {
        Debug.Log("미션1 클리어");
        //UIManager에서 hideUI만들어서 UI 숨기기 
        //보드게임 주사위 굴리는 원래 상태로 돌아가기
        //GameManager.Instance.SetState(GameState.TurnEnd);
    }

    //mission 마무리 후 Destroy로 list에서 삭제시키기
}
