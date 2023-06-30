using UnityEngine;

public interface IProjectile {

    float Damage { get; }
    
    float LifeTime { get; }

    GameObject ImpactEffect { get; }
    
    GameObject Target { get; }

    void SetTarget(GameObject target);
}