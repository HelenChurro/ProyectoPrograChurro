using UnityEngine;

public class Arbol : MonoBehaviour, IDanable
{
    [field: SerializeField] public int Health { get; set; } = 5;
    [SerializeField] private GameObject resourcePrefab;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log("Auch mis hojitas Vida: " + Health);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("xb arbolito");

        if (resourcePrefab != null)
        {
            Instantiate(resourcePrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
