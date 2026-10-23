using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private NavMeshAgent agent;


    private void Update()
    {
        //Deteccion de jugador
        if (Physics.CheckSphere(transform.position, detectionRadius, playerMask))
        {
            agent.SetDestination(FindAnyObjectByType<PlayerController>().transform.position);
        }
        //Ataque al jugador
        if (Physics.CheckSphere(transform.position, attackRadius, playerMask))
        {
            Vector3 direction = FindAnyObjectByType<PlayerController>().transform.position - transform.position;
            Ray attackRay = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction * attackRadius, Color.red);
            if (Physics.Raycast(attackRay, attackRadius, playerMask))
            {
                Debug.Log("hit player");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
