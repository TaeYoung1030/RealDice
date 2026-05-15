using UnityEngine;

public class AttackState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("현재 상태 : getup");
    }

    public void ExitState(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }

   
}
