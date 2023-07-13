using System.Collections;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject[] paths;
    
    private float _countdown = 0;
    private int _waveIndex = 0;
    private float _accEnemyLifetimes = 0;
    private int _destroyedTowers = 0;
    private float _accTowerLifetimes = 0;
    private bool bossSound = true;

    public int CurrentWave => _waveIndex + 1;
    public int TotalWaves => waves.Length;
    
    public int KilledEnemies = 0;

    public int CurrentEnemies => TotalEnemies;
    public int TotalEnemies => waves.Sum(wave => wave.count);

    public float AvgEnemyLifetime => KilledEnemies > 0 ? _accEnemyLifetimes / KilledEnemies : 0;
    public float AvgTurretLifetime => _destroyedTowers > 0 ? _accTowerLifetimes / _destroyedTowers : 0;

    void Start () {
        _countdown = waves[_waveIndex].countdown;

        EventManager.instance.OnEnemyDeath += OnEnemyDeath;
        EventManager.instance.OnLivesChange += OnEnemyVictory;
        EventManager.instance.OnTowerDestroyed += OnTowerDestroyed;
    }

    void Update () {
        if (_waveIndex == waves.Length)
            return;

        if (_waveIndex == waves.Length - 1 && _countdown <= 4f && bossSound)
        {
            EventManager.instance.BossWave();
            bossSound = false;
        }
        
        if (_countdown <= 0f) {
            StartCoroutine(SpawnWave());
            
            _countdown = (_waveIndex < waves.Length-1 ? waves[_waveIndex+1].countdown : 0) + waves[_waveIndex].Duration;
            _destroyedTowers = 0;

            _waveIndex++;
            return;
        }

        _countdown -= Time.deltaTime;   
    }    
    
    IEnumerator SpawnWave () {
        EventManager.instance.WaveChange(_waveIndex+1, waves.Length);
        Wave wave = waves[_waveIndex];

        for (int i = 0; i < wave.count; i++) {
            GameObject enemyObj = Instantiate(wave.enemy, transform.position, transform.rotation);
            if(wave.path == null)
                enemyObj.GetComponent<Enemy>().SetPath(paths[Random.Range(0, paths.Length)]);
            else
                enemyObj.GetComponent<Enemy>().SetPath(wave.path);
            
            yield return new WaitForSeconds(1f / wave.rate);
        }
    }

    private void OnEnemyDeath(float lifetime) {
        KilledEnemies += 1;
        _accEnemyLifetimes += lifetime;
    }
    
    private void OnEnemyVictory(int lives) {
        KilledEnemies += 1;
    }

    private void OnTowerDestroyed(float lifetime) {
        _destroyedTowers += 1;
        _accTowerLifetimes += lifetime;
    }

    [System.Serializable]
    public class Wave {
        public GameObject enemy; 
        public GameObject path;
        public int count;
        public float rate;
        public int countdown;
        public bool enabled;
        
        public float Duration {
            get { return count / rate; }
        }
    }

}
