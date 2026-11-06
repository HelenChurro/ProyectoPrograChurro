using UnityEngine;

public class EnemyState
{
    public StateMachine stateMachine;
    public AdvancedEnemy advancedEnemy;

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }

    public EnemyState(AdvancedEnemy _advancedEnemy, StateMachine _stateMachine)
    {
        this.advancedEnemy = _advancedEnemy;
        this.stateMachine = _stateMachine;
    }
}
