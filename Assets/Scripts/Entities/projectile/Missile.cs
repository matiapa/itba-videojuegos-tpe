using UnityEngine;

public class Missile : Projectile {

    [SerializeField] private float _speed = 30f;

    void FixedUpdate() {
        Transform targetMuzzle = _target.transform.Find("Target muzzle");
        if (targetMuzzle == null)
            targetMuzzle = _target.transform;

        Vector3 dir = targetMuzzle.position - transform.position;

        bool enemyTarget = _target.GetComponent<Enemy>() != null;
        Debug.DrawRay(transform.position, dir, enemyTarget ? Color.green : Color.red, 1);

        if (dir.magnitude > 1)
	        _movementController.move(dir, _speed);
        else if (dir.magnitude > 0)
            _movementController.move(dir, 1 / Time.deltaTime); 
    }
}