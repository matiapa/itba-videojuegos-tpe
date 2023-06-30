using UnityEngine;

public class Bullet : Projectile {

    [SerializeField] private float _shootingForce = 50f; 
    
    void Start() {
        Transform targetMuzzle = _target.transform.Find("Target muzzle");
        if (targetMuzzle == null)
            targetMuzzle = _target.transform;

        Vector3 dir =  targetMuzzle.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = rotation;

        bool enemyTarget = _target.GetComponent<Enemy>() != null;
        Debug.DrawRay(transform.position, dir, enemyTarget ? Color.green : Color.red, 10);

        GetComponent<Rigidbody>().AddForce(_shootingForce * dir.normalized, ForceMode.VelocityChange);
    }
}