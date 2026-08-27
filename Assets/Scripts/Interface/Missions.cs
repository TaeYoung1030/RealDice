using UnityEngine;

public interface Missions
{
    //미션 시작할때 호출
    void StartMission();
    void FailMission();
    void EndMission();
}
