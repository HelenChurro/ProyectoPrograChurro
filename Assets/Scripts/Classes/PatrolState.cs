using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    private Vector3 targetPosition;
    public PatrolState(AdvancedEnemy _advancedEnemy, StateMachine _stateMachine) : base(_advancedEnemy, _stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        GetRandomPosition();
        advancedEnemy.agent.SetDestination(targetPosition);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if ((advancedEnemy.transform.position - targetPosition).magnitude < 0.1f)
        {
            GetRandomPosition();
            advancedEnemy.agent.SetDestination(targetPosition);
        }
    }


    public void GetRandomPosition()
    {
        bool foundPosition = false;
        while (!foundPosition)
        {
            Vector3 _position = advancedEnemy.transform.position + (Vector3)UnityEngine.Random.insideUnitSphere * advancedEnemy.randomMovementRange;
            _position.y = 0;

            if (NavMesh.SamplePosition(_position, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
            {
                targetPosition = _position;
                foundPosition = true;
            }
        }
    }
}
