using System.Collections;
using UnityEngine;

public class PoisonBullet : Bullet
{
    [SerializeField] private float _poisonDamage = 1f;
    [SerializeField] private int _numTimes = 10;
    [SerializeField] private float _repeatInterval = 1f;


    protected override void OnCollisionEnter(Collision collision) {
        if(collision.gameObject != _target)
            return;
        
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null) {
            StartCoroutine(CreateCmdApplyDamage(damageable));
        } 

        base.OnCollisionEnter(collision);
    }
    
    IEnumerator CreateCmdApplyDamage (IDamageable damageable) {
        for (int i = 0; i < _numTimes; i++) {
            CommandQueue.instance.AddEvent(new CmdApplyDamage(damageable, _poisonDamage));
            yield return new WaitForSeconds(_repeatInterval);
        }
        Destroy(this.gameObject, _numTimes * _repeatInterval);
    }
}
