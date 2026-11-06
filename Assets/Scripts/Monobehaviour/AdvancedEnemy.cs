using Unity.IO.LowLevel.Unsafe;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedEnemy : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public LayerMask HitBy { get; set; }

    public StateMachine stateMachine;
    public PatrolState patrolState;
    public ChaseState chaseState;
    public ConfussionState confussionState;
    public AttackState attackState;

    public NavMeshAgent agent;

    [Header("Patrol Variables")]
    public float randomMovementRange = 5f;

    [Header("Chase Variables")]
    public float detectionRadius;
    public LayerMask playerMask;
    public PlayerController player;

    [Header("Confussion Varibales")]
    

    [Header("Attack Variables")]
    public float attackRadius;

    private void Awake()
    {
        stateMachine = new StateMachine();

        patrolState = new PatrolState(this, stateMachine);
        chaseState = new ChaseState(this, stateMachine);
        attackState = new AttackState(this, stateMachine);
        confussionState = new ConfussionState(this, stateMachine);

        player = FindAnyObjectByType<PlayerController>();

        stateMachine.Initialize(patrolState);
    }

}
