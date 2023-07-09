using UnityEngine;

public class FillingLifeController : BasicLifeController {
    [SerializeField] private float _fillRate = 1/5f;

    void Update() {
        _currentLife += _fillRate * Time.deltaTime;
        if (_currentLife > _maxLife)
            _currentLife = _maxLife;
    }
}