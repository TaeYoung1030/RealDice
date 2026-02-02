using UnityEngine;

public class MonMission1 : MonoBehaviour,Missions
{
   public void StartMission()
   {
        Debug.Log("미션1 진행중");
   }

    //mission 마무리 후 Destroy로 list에서 삭제시키기
}
