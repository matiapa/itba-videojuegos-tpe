using System;

public interface IDamageable {
    float MaxLife { get; }

    float CurrentLife { get; }

    void TakeDamage(float damage);

    public event Action OnDeath;
}