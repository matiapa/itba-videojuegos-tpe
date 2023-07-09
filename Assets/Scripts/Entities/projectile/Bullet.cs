using UnityEngine;
public class Bullet : Projectile {

    [SerializeField] private float _initialSpeed = 50f; 
    
    protected override void Start() {
        base.Start();

        Transform targetMuzzle = _target.transform.Find("Target muzzle");
        if (targetMuzzle == null)
            targetMuzzle = _target.transform;

        Vector3 dir =  targetMuzzle.position - transform.position;

        bool enemyTarget = _target.GetComponent<Enemy>() != null;
        Debug.DrawRay(transform.position, dir, enemyTarget ? Color.green : Color.red, 1);

        _movementController.move(dir, _initialSpeed);
    }
}