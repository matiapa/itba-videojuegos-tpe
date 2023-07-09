using UnityEngine;

[RequireComponent(typeof(IMovable))]
public class Projectile : MonoBehaviour, IProjectile {
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private GameObject _impactEffect;

    protected GameObject _target;
    protected IMovable _movementController;

    public float Damage => _damage;
    public float LifeTime => _lifetime;
    public GameObject ImpactEffect => _impactEffect;
    public GameObject Target => _target;

    void Awake() {
        _movementController = GetComponent<IMovable>();
	}

    protected virtual void OnCollisionEnter(Collision collision) {        
        if(collision.gameObject != _target) {
            Destroy(this.gameObject);
            return;
        }

        if (_impactEffect != null) {
            GameObject effectIns = Instantiate(_impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2.5f);
        }

        Destroy(this.gameObject);
    }

    protected virtual void Start() {
        if(_target == null)
            Destroy(this.gameObject);

        // For better playablity, damage is applied on instantiation, collision is used just for animation purposes

        IDamageable damagable = _target.GetComponent<IDamageable>();
        if (damagable != null) {
            CommandQueue.instance.AddEvent(new CmdApplyDamage(damagable, _damage));
        }
    }

    protected virtual void Update() {
        if(_target == null)
            Destroy(this.gameObject);

        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject newTarget) {
        _target = newTarget;
    }
}