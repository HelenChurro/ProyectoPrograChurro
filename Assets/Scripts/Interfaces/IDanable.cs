using UnityEngine;

public interface IDanable
{
    int Health { get; set; }
    void TakeDamage(int damage);
}