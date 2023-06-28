public class CmdApplySlowEffect : ICommand
{
    private ISlowable _target;
    private float _slowDuration;
    private float _slowFactor;
    private float _originalSpeed;

    public CmdApplySlowEffect(ISlowable target, float slowFactor, float slowDuration)
    {
        _target = target;
        _slowDuration = slowDuration;
        _slowFactor = slowFactor;
    }

    public void Execute()
    {
        _target.TakeSlowEffect(_slowFactor, _slowDuration);
    }
}
