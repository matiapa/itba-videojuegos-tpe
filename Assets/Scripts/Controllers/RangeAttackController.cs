using System;
using System.Linq;
using UnityEngine;

public class RangeAttackController : MonoBehaviour {

    [SerializeField] private float _maxRange = 50f;
    [SerializeField] private float _shootingCooldown = 2f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private bool _isEnemy;

    private GameObject nearestEnemy = null;
    private int _currentBulletCount;
    private float _currentShotCooldown;


    private void Start() {
        _currentShotCooldown = _shootingCooldown;
    }

    private void Update() {

        // Find nearest enemy
        
        if (_isEnemy) {
            nearestEnemy = GameObject.FindObjectsOfType<Turret>()
                .Where(enemy => !enemy.IsDead && Vector3.Distance(transform.position, enemy.transform.position) <= _maxRange)
                .Select(enemy => enemy.gameObject)
                .FirstOrDefault();
        } else {
            nearestEnemy = GameObject.FindObjectsOfType<Enemy>()
                .Where(enemy =>
                    !enemy.IsDead && Vector3.Distance(transform.position, enemy.transform.position) <= _maxRange)
                .Select(enemy => enemy.gameObject)
                .FirstOrDefault();

            // Look towards the enemy
            
            if (nearestEnemy != null) {
                Quaternion lookRotation = Quaternion.LookRotation(nearestEnemy.transform.position - transform.position);
                Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
                this.transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
            }
        }
        
        if (_currentShotCooldown >= 0)
            _currentShotCooldown -= Time.deltaTime;
        else {
            if (nearestEnemy == null)
                return;

            // Instantiate the projectile and set its target
            
            Transform originMuzzle = gameObject.transform.Find("Origin muzzle");
            if (originMuzzle == null)
                originMuzzle = transform;

            var projectile = Instantiate(_projectilePrefab, originMuzzle.position, originMuzzle.rotation);
        
            projectile.GetComponent<IProjectile>().SetTarget(nearestEnemy);

            // Notify of attack performed event

            EventManager.instance.Attack(this.gameObject);
            
            _currentShotCooldown = _shootingCooldown;
        }
    }

    public void SetIsEnemy(bool isEnemy) {
        _isEnemy = isEnemy;
    }
}