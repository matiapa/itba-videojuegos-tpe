using UnityEngine;

public interface IProjectile {

    float Damage { get; }
    
    float LifeTime { get; }

    GameObject ImpactEffect { get; }
    
    GameObject Target { get; }

    void OnTriggerEnter(Collider collider);

    void SetTarget(GameObject target);
}