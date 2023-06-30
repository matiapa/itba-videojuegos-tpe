using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(WaveManager))]
public class GameManager : MonoBehaviour {
    [SerializeField] private int _initialLives;
    [SerializeField] private int _initialCoins;
    [SerializeField] private float _timeScale = 1;
    
    private int _lives;
    private int _coins;
    private WaveManager _waveManager;

    public event Action<int> OnNetCoinChange;
    public event Action<int> OnNetLivesChange;

    public int Lives => _lives;
    public int Coins => _coins;

    public int CurrentWave => _waveManager.CurrentWave;
    public int MaxWave => _waveManager.MaxWave;
    static public GameManager instance;

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;

        Time.timeScale = _timeScale;
    }

    private void Start() {
        _waveManager = GetComponent<WaveManager>();

        EventManager.instance.OnCoinChange += OnCoinChange;
        EventManager.instance.OnLivesChange += OnLivesChange;
        EventManager.instance.OnGameOver += OnGameOver;

        _lives = _initialLives;
        _coins = _initialCoins;
    }

    private void Update() {
        if (_waveManager.LastWave && GameObject.FindObjectsOfType<Enemy>().Length == 0)
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
            SceneManager.LoadScene("VictoryScene");
        else
            SceneManager.LoadScene("DefeatScene");
    }
}