using UnityEngine;

public class Missile : Projectile {

    [SerializeField] private float _speed = 30f;

    void FixedUpdate() {
        if (_target == null) {
            Destroy(gameObject);
            return;
        }

        Transform targetMuzzle = _target.transform.Find("Target muzzle");
        if (targetMuzzle == null)
            targetMuzzle = _target.transform;

        Vector3 dir = targetMuzzle.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = rotation;

        Debug.DrawRay(transform.position, dir, Color.green, 1);
        
        GetComponent<Rigidbody>().AddForce(_speed * dir.normalized, ForceMode.VelocityChange);
    }

    // protected override void Update() {
    //     Vector3 dir = _target.transform.position - transform.position;

    //     Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
    //     transform.rotation = rotation;
        
    //     if (dir.magnitude > 1)
	//         transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
    //     else if (dir.magnitude > 0)
    //         transform.Translate(dir, Space.World);

    //     base.Update();
    // }
}