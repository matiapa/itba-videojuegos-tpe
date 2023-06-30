using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BuildController))]
public class Bomb : MonoBehaviour {

    [SerializeField] private float _radius = 30f;
    [SerializeField] private float _force = 10f;
    [SerializeField] private float _damage = 10f;
    public GameObject explosion;

    public void OnCollisionEnter(Collision collision) {
        if (! collision.gameObject.GetComponent<Enemy>())
            return;

        GameObject[] nearEnemies = GameObject.FindObjectsOfType<Enemy>()
            .Where(enemy => !enemy.IsDead && Vector3.Distance(transform.position, enemy.transform.position) <= _radius)
            .Select(enemy => enemy.gameObject)
            .ToArray();

        foreach (GameObject enemy in nearEnemies) {
            IDamageable damagable = enemy.GetComponent<IDamageable>();
            if (damagable != null) {
                CommandQueue.instance.AddEvent(new CmdApplyDamage(damagable, _damage));
            }

            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(_force, transform.position, _radius, 3.0F, ForceMode.Impulse);
            }
        }

        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);
        
        Destroy(this.gameObject);
    }

}