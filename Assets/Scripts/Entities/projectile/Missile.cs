using UnityEngine;

public class Missile : Projectile {

    [SerializeField] private float _speed = 30f;

    protected override void Update() {
        Vector3 dir = _target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = rotation;
        
        if (dir.magnitude > 1)
	        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
        else if (dir.magnitude > 0)
            transform.Translate(dir, Space.World);

        base.Update();
    }
}