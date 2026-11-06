using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(AdvancedEnemy _advancedEnemy, StateMachine _stateMachine) : base(_advancedEnemy, _stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        advancedEnemy.agent.SetDestination(advancedEnemy.player.transform.position);
    }
}
