using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private float _collidingForce = 5f;
    [SerializeField] private GameObject _impactEffect;

    protected GameObject _target;

    public float Damage => _damage;
    public float LifeTime => _lifetime;
    public GameObject ImpactEffect => _impactEffect;
    public GameObject Target => _target;

    public void OnTriggerEnter(Collider collider) {
        if(collider.gameObject != _target)
            return;
        
        IDamageable damagable = collider.gameObject.GetComponent<IDamageable>();
        if (damagable != null) {
            CommandQueue.instance.AddEvent(new CmdApplyDamage(damagable, _damage));
        }

        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        if (rb != null) {
            Vector3 dir = _target.transform.position - transform.position;
            dir.y = 0;  // It would throw characters underground otherwise!
            rb.AddForce(_collidingForce * dir.normalized, ForceMode.Impulse);
        }

        if (_impactEffect != null) {
            GameObject effectIns = Instantiate(_impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2.5f);
        }

        Destroy(this.gameObject);
    }

    protected virtual void Update() {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0) {
            Destroy(this.gameObject);
        }

        if(_target == null) {
            Destroy(this.gameObject);
            return;
        }
    }

    public void SetTarget(GameObject newTarget) {
        _target = newTarget;
    }
}