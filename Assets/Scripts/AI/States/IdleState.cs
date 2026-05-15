using UnityEngine;

public class IdleState : IEnemyState
{
    [SerializeField] float NextState = 1f;
    private Animator animator;
    float Timer;
    public void EnterState(EnemyStateManager enemy)
    {
        animator = enemy.GetComponent<Animator>();

        animator.SetFloat("Walk", 0f);
        Timer = 0f;
    }

    public void ExitState(EnemyStateManager enemy)
    {
       
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        Timer += Time.deltaTime;

        if(Timer >= NextState)
        {
            //일정 시간 후에 
            enemy.TransitionToState(new PatrolState());            
        }
    }  
}
