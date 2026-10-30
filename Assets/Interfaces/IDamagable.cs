using UnityEngine;

public interface IDamagable
{
    public int Health { get; set; }
    public LayerMask HitBy { get; set; }

    public virtual void TryDamage(int newDamage, LayerMask attacker)
    {
        if ((HitBy.value & attacker.value) != 0) { return; }
        if (Health <= 0) { return; }

        Health = newDamage;
        if (Health <= 0)
        {
            Die();
        }
    }
    public void Die();

}
