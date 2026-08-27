using UnityEngine;
using UnityEngine.AI;

public class ShockState : IEnemyState
{
    private NavMeshAgent agent;
    private float shockTimer;
    private float shockDuration = 3.7f;
    public void EnterState(EnemyStateManager enemy)
    {
        agent = enemy.GetComponent<NavMeshAgent>();
        shockTimer = 0f;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        enemy.GetComponent<Animator>().SetFloat("Walk", 0f);

        enemy.GetComponent<Animator>().SetTrigger("doShock");
        Debug.Log("몬스터 스턴");
    }

    public void ExitState(EnemyStateManager enemy)
    {
        if(agent != null)
        {
            agent.isStopped = false;
        }
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        shockTimer += Time.deltaTime;

        if(shockTimer >= shockDuration)
        {
            enemy.TransitionToState(new ChaseState());
        }
    }
}
