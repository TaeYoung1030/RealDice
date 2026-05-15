using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    
    private NavMeshAgent agent;
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.GetComponent<Animator>().SetFloat("Walk", 6f);
        agent = enemy.GetComponent<NavMeshAgent>();
        Debug.Log("플레이어를 쫓기 시작합니다");
    }

    public void ExitState(EnemyStateManager enemy)
    {
        
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        agent.SetDestination(enemy.player.position);
    }
}
