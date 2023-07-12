using System;
using Microlight.MicroBar;
using UnityEngine;

public class BasicLifeController : MonoBehaviour, IDamageable {

    [SerializeField] protected float _maxLife = 100f;

    [SerializeField] MicroBar _hpBar;

    protected float _currentLife;

    public event Action OnDeath;

    public float MaxLife => _maxLife;
    public float CurrentLife => _currentLife;
    public bool IsDead => _currentLife <= 0;

    void Start() {
        _currentLife = _maxLife;
        
        if (_hpBar != null) 
            _hpBar.Initialize(_maxLife);
    }

    public void TakeDamage(float damage) {        
        if (_currentLife <= 0)
            return;
        
        _currentLife -= damage;

        if (_hpBar != null)
            _hpBar.UpdateHealthBar(_currentLife);

        if(_currentLife <= 0)
            if (OnDeath != null) OnDeath();
    }
}