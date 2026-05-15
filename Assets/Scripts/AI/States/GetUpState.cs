using UnityEngine;

public class GetUpState : IEnemyState
{
    float Timer;
    public void EnterState(EnemyStateManager enemy)
    {
        Timer = 0f;
    }

    public void ExitState(EnemyStateManager enemy)
    {
       
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        Timer += Time.deltaTime;

        if(Timer >= 6.5f)
        {           
            enemy.TransitionToState(new IdleState());
        }
    }
}
