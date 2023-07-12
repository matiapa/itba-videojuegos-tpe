using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFollowerController))]
[RequireComponent(typeof(BasicLifeController))]
[RequireComponent(typeof(RangeAttackController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

    [SerializeField] private int _coinsEarnedOnDeath;
    [SerializeField] private int _livesRemovedOnArrival;    
    [SerializeField] private SoundStats _deathSound;

    private PathFollowerController _pathFollowerController;
    private BasicLifeController _basicLifeController;
    private RangeAttackController _rangeAttackController;
    private AudioSource _audioSource;
    private Animator _animator;
    private float _lifetime = 0;
    
    public bool IsDead => _basicLifeController.IsDead;

    void Awake() {
        _pathFollowerController = GetComponent<PathFollowerController>();

        _basicLifeController = GetComponent<BasicLifeController>();
        _basicLifeController.OnDeath += OnDeath;

        _rangeAttackController = GetComponent<RangeAttackController>();
        _rangeAttackController.SetIsEnemy(true);

        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    void Update() {
        if (_pathFollowerController.endReached) {
            EventManager.instance.LivesChange(-_livesRemovedOnArrival);
            Destroy(gameObject);
        }

        _lifetime += Time.deltaTime;
    }

    private void OnDeath() {
        EventManager.instance.EnemyDeath(_lifetime);
        EventManager.instance.CoinChange(_coinsEarnedOnDeath);

        _audioSource.PlayOneShot(_deathSound.AudioClip);
        _animator.SetTrigger("Die");

        Destroy(this.gameObject, 0.8f);
    }
    
    public void SetPath(GameObject _pathContainer) {
       _pathFollowerController.SetPath(_pathContainer);
    }
}
