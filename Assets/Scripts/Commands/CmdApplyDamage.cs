public class CmdApplyDamage : ICommand {
    private IDamageable _damageable;
    private float _damage;

    public CmdApplyDamage(IDamageable damagable, float damage) {
        _damageable = damagable;
        _damage = damage;
    }

    public void Execute() => _damageable.TakeDamage(_damage);
}
