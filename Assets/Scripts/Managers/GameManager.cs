using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(WaveManager))]
public class GameManager : MonoBehaviour {
    [SerializeField] private int _initialLives;
    [SerializeField] private int _initialCoins;
    [SerializeField] private float _timeScale = 1;
    
    private int _lives;
    private int _coins;
    private WaveManager _waveManager;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    
    public event Action<int> OnNetCoinChange;
    public event Action<int> OnNetLivesChange;

    public int Lives => _lives;
    public int Coins => _coins;

    public int CurrentWave => _waveManager.CurrentWave;

    public WaveManager.Wave CurrentWaveInfo => _waveManager.WaveInfo;
    public int MaxWave => _waveManager.TotalWaves;
    public int CurrentEnemies => _waveManager.CurrentEnemies;
    public int TotalEnemies => _waveManager.TotalEnemies;
    public float AvgEnemyLifetime => _waveManager.AvgEnemyLifetime;
    public float AvgTurretLifetime => _waveManager.AvgTurretLifetime;

    static public GameManager instance;

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
        _audioSource = GetComponent<AudioSource>();
        Time.timeScale = _timeScale;
        _waveManager = GetComponent<WaveManager>();
    }

    private void Start() {
        EventManager.instance.OnCoinChange += OnCoinChange;
        EventManager.instance.OnLivesChange += OnLivesChange;
        EventManager.instance.OnGameOver += OnGameOver;
        EventManager.instance.OnBossWave += OnBossWave;
        _lives = _initialLives;
        _coins = _initialCoins;
    }

    private void Update() {
        if (_waveManager.TotalEnemies == _waveManager.KilledEnemies)
            EventManager.instance.GameOver(true);
    }

    private void OnCoinChange(int coinChange) {
        _coins += coinChange;
        
        if(OnNetCoinChange != null) OnNetCoinChange(_coins);
    }

    private void OnLivesChange(int livesChange) {
        _lives += livesChange;

        if(OnNetLivesChange != null) OnNetLivesChange(_lives);

        if(_lives <= 0)
            EventManager.instance.GameOver(false);
    }

    private void OnGameOver(bool isVictory) {
        if (isVictory) 
            StartCoroutine(LoadVictorySceneAfterDelay());
        else
            SceneManager.LoadScene("DefeatScene");
    }

    private IEnumerator LoadVictorySceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("VictoryScene");
    }

    public void PauseGame(bool pause) {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = _timeScale;
    }
    
    void OnBossWave() {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_audioClip);
    }

}