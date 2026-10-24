using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDanable
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int damage = 10;

    [field: SerializeField] public int Health { get; set; } = 20;

    [Header("Tipo de Enemigo")]
    public bool isNightBoss = false; // NocheDia

    [SerializeField] private GameObject pickUpPrefab;
    [Range(0f, 1f)][SerializeField] private float dropChance = 1f;

    private void Update()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (isNightBoss || distanceToPlayer <= detectionRadius)
        {
            agent.SetDestination(player.transform.position);
        }

        if (distanceToPlayer <= attackRadius)
        {
            Vector3 direction = player.transform.position - transform.position;
            Ray attackRay = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction * attackRadius, Color.red);
            if (Physics.Raycast(attackRay, attackRadius, playerMask))
            {
                player.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        Debug.Log("Hit enemi " + Health);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (pickUpPrefab != null && Random.value <= dropChance)
        {
            Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
        }

        //Semuere
        DayNightManager manager = FindAnyObjectByType<DayNightManager>();
        if (manager != null)
        {
            manager.OnEnemyKilled(this);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}