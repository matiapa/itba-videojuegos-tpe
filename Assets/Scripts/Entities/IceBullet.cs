using UnityEngine;

public class IceBullet : Bullet
{
    [SerializeField] private float _slowRate = 0.5f;
    [SerializeField] private float _slowDuration = 5f;
    public new void OnTriggerEnter(Collider collider) {
        
        ISlowable slowable = collider.gameObject.GetComponent<ISlowable>();
        if (slowable != null) {
            CommandQueue.instance.AddEvent(new CmdApplySlowEffect(slowable,_slowRate, _slowDuration));
        }
        base.OnTriggerEnter(collider);
    }
}   
    
