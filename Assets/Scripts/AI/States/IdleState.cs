using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("현재 상태 : IDLE");
    }

    public void ExitState(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("상태 변환");
    }

   
}
