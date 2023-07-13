using System;
using UnityEngine;

public class IceBullet : Bullet
{
    [SerializeField] private float _slowRate = 0.5f;
    [SerializeField] private float _slowDuration = 5f;
    
    protected override void OnTriggerEnter(Collider other) {
        if(other.gameObject != _target)
            return;

        ISlowable slowable = other.gameObject.GetComponent<ISlowable>();
        if (slowable != null) {
            CommandQueue.instance.AddEvent(new CmdApplySlowEffect(slowable,_slowRate, _slowDuration));
        }

        base.OnTriggerEnter(other);
    }
}   
    
