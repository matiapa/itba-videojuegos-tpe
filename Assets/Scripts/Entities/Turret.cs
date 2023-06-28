using UnityEngine;

[RequireComponent(typeof(FillingLifeController))]
[RequireComponent(typeof(RangeAttackController))]
[RequireComponent(typeof(BuildController))]
[RequireComponent(typeof(AudioSource))]
public class Turret : MonoBehaviour {

	private FillingLifeController _fillingLifeController;
    private RangeAttackController _rangeAttackController;
    private BuildController _buildController;
    public bool IsDead => _fillingLifeController.IsDead;
    public FillingLifeController FillingLifeController => _fillingLifeController;
    
    private AudioSource _audioSource;

    [SerializeField] private SoundStats _canonShot;
    [SerializeField] private SoundStats _deathSound;

    void Awake() {
        _fillingLifeController = GetComponent<FillingLifeController>();
        _fillingLifeController.OnDeath += OnDeath;

        _rangeAttackController = GetComponent<RangeAttackController>();
        _buildController = GetComponent<BuildController>();
        _audioSource = GetComponent<AudioSource>();
        _rangeAttackController.SetIsEnemy(false);

        _audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        EventManager.instance.OnAttack += OnAttack;
    }

    void OnAttack(GameObject attacker) {
        if (attacker == this.gameObject)
            _audioSource.PlayOneShot(_canonShot.AudioClip);
    }
    
    private void OnDeath() {
        EventManager.instance.OnAttack -= OnAttack; 
        _audioSource.PlayOneShot(_deathSound.AudioClip);
        // TODO: animacion de destruccion
        _rangeAttackController.enabled = false;
        transform.position = new Vector3(transform.position.x, -200, transform.position.z);
        Invoke("DestroyTurret", 2f);
    }

    private void DestroyTurret() {
        Destroy(this.gameObject);
    }
}
