using System.Collections;
using UnityEngine;

public class PoisonBullet : Bullet
{
    [SerializeField] private float _poisonDamage;
    [SerializeField] private int _numTimes;
    [SerializeField] private float _repeatInterval;
    public new void OnTriggerEnter(Collider collider) {
        
        if(collider.gameObject != _target)
            return;
        
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            StartCoroutine(CreateCmdApplyDamage(damageable));
        }
        
    }
    
    IEnumerator CreateCmdApplyDamage (IDamageable damageable) {
        for (int i = 0; i < _numTimes; i++)
        {
            CommandQueue.instance.AddEvent(new CmdApplyDamage(damageable, _poisonDamage));
            yield return new WaitForSeconds(_repeatInterval);
        }
        Destroy(this.gameObject);
    }
}
