using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Traps : MonoBehaviour, IDamagable
{
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public LayerMask HitBy { get; set; }

    [SerializeField] private ParticleSystem explosion;

    public void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        List<Collider> explosionHit = Physics.OverlapSphere(transform.position, 3f).ToList();
        foreach (Collider collider in explosionHit)
        {
            if (collider.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.TryDamage(1, 3);
                damagable.TryDamage(1, 6);
            }
        }
        gameObject.SetActive(false);
    }
}
