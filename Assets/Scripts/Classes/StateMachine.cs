using UnityEngine;

public class StateMachine
{
    public EnemyState currentState;

    public void Initialize(EnemyState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
