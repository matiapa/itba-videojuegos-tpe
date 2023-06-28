using UnityEngine;

public interface IBullet {

    float Damage { get; }

    float Speed { get;  }
    
    float LifeTime { get; }

    GameObject ImpactEffect { get; }
    
    GameObject Target { get; }

    void Travel();

    void OnTriggerEnter(Collider collider);

    void SetTarget(GameObject target);
}