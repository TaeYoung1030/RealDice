
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    Ready,
    Rolling,
    Moving,
    Mission,
    TurnEnd
}
public class GameManager : MonoBehaviour
{
    //싱글톤 패턴 사용
    public static GameManager instance;
    
    //상태 패턴 사용 
    public GameState CurrentState {  get; private set; }
    [SerializeField] List<GameObject> missionPrefabs;

    private GameObject activeMissionObject;
    private Missions activeMission;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);

        SetState(GameState.Ready);
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"[GameManager] 상태 변경: {CurrentState}");
    }

    public bool CanActivity()
    {
        return CurrentState == GameState.Ready;
    }

    public void OnArriveTile()
    {
        Debug.Log("타일 도착! 미션 발생!");
        StartRandomMission();
    }

    void StartRandomMission()
    {
        SetState(GameState.Mission);

        if(missionPrefabs.Count > 0 )
        {
            //랜덤 뽑기
            int rn = Random.Range(0,missionPrefabs.Count);
            //복제품 생성하기
            activeMissionObject = Instantiate(missionPrefabs[rn]);
            //리스트에서 뽑은 미션은 제거(중복방지)
            missionPrefabs.RemoveAt(rn);
            //미션 시작하기 
            activeMission = activeMissionObject.GetComponent<Missions>();
            if(activeMission != null) activeMission.StartMission();
        }
        else
        {
            Debug.Log("미션 프리팹이 없습니다!");
            CompleteMission();
        }
    }

    public void OnPlayerFail()
    {
        if(activeMission != null)
        {
            activeMission.FailMission();
        }
    }

    public void CompleteMission()
    {
        Debug.Log("미션완료! 대기 상태로 복귀합니다");
        SetState(GameState.Ready);
    }

    public void ClearActiveMission()
    {
        activeMission = null;

        if(activeMissionObject != null) Destroy(activeMissionObject);

        activeMissionObject= null;
    }
}
