using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] GameObject monster;
    public IEnemyState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TransitionToState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void TransitionToState(IEnemyState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);

    }

}
