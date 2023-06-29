using UnityEngine;

public class Bullet : Projectile {

    [SerializeField] private float _shootingForce = 50f; 

    void Start() {
        Vector3 dir = _target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = rotation;

        GetComponent<Rigidbody>().AddForce(_shootingForce * dir.normalized, ForceMode.Impulse);
    }
}