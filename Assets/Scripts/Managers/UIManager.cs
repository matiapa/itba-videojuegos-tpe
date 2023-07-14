using TMPro;
using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject _coinsTextMesh;
    [SerializeField] private GameObject _livesTextMesh;
    [SerializeField] private GameObject _timerTextMesh;
    [SerializeField] private GameObject _currentEnemyHpTextMesh;
    [SerializeField] private GameObject _currentEnemySpeedTextMesh;
    [SerializeField] private GameObject _currentEnemyCountTextMesh;
    [SerializeField] private GameObject _currentEnemyNameTextMesh;
    [SerializeField] private GameObject _currentEnemyImage;
    
    [SerializeField] private GameObject _waveTextMesh;
    [SerializeField] private GameObject _enemiesTextMesh;
    
    [SerializeField] private GameObject _basicTurretTextMesh;
    [SerializeField] private GameObject _iceTurretTextMesh;
    [SerializeField] private GameObject _poisonTurretTextMesh;
    [SerializeField] private GameObject _missileTurretTextMesh;
    [SerializeField] private GameObject _bombTextMesh;

    [SerializeField] private GameObject _avgEnemyLifeTimeTextMesh;
    [SerializeField] private GameObject _avgTurretLifeTimeTextMesh;

    private bool _showingMenu = false;

    private int[] _turretsCost;

    private void Awake()
    {
        EventManager.instance.OnTimerChanged += UpdateTimer;
    }

    private void Start() {
        GameManager.instance.OnNetCoinChange += UpdateCoinsValue;
        GameManager.instance.OnNetLivesChange += UpdateLivesValue;
        EventManager.instance.OnWaveChange += UpdateCurrentWave;
        EventManager.instance.OnEnemyDeath += UpdateCurrentEnemies;

        _turretsCost = BuildingSystem.instance.EntitiesCost;
        _basicTurretTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[0]}";
        _iceTurretTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[1]}";
        _poisonTurretTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[2]}";
        _missileTurretTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[3]}";
        _bombTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[4]}";
        
        UpdateCoinsValue(GameManager.instance.Coins);
        UpdateLivesValue(GameManager.instance.Lives);
        UpdateCurrentWave(GameManager.instance.CurrentWave, GameManager.instance.MaxWave, GameManager.instance.CurrentWaveInfo);
        UpdateTimer(GameManager.instance.CurrentWaveInfo.countdown);
    }

    private void Update() {
        /*
        if (_showingMenu && Input.GetButtonUp("Accept")) {
            print("Hiding menu");
            GameObject menu = GameObject.Find("End wave stats");
            menu.transform.Translate(-1000 * Vector3.up, Space.World);

            GameManager.instance.PauseGame(false);

            _showingMenu = false;
        }
        */
    }

    private void UpdateCoinsValue(int newCoins) {
        if (_coinsTextMesh != null) 
            _coinsTextMesh.GetComponent<TextMeshProUGUI>().text = $"{newCoins}";
    }

     private void UpdateLivesValue(int newLives) {
        if (_livesTextMesh != null)
            _livesTextMesh.GetComponent<TextMeshProUGUI>().text = $"{newLives}";
    }
     
    private Coroutine _timerCoroutine;

    private void UpdateCurrentWave(int currentWave, int maxWave, WaveManager.Wave waveInfo) {
        //if(currentWave != 1)
          //  DisplayWaveEndStats();

        if (_waveTextMesh != null)
            _waveTextMesh.GetComponent<TextMeshProUGUI>().text = $"{currentWave} / {maxWave}";

        if (waveInfo != null)
        {
            if (_currentEnemyHpTextMesh != null)
                _currentEnemyHpTextMesh.GetComponent<TextMeshProUGUI>().text = $"{waveInfo.EnemyHp}";
        
            if (_currentEnemySpeedTextMesh != null)
                _currentEnemySpeedTextMesh.GetComponent<TextMeshProUGUI>().text = $"{waveInfo.EnemySpeed}";
        
            if (_currentEnemyCountTextMesh != null)
                _currentEnemyCountTextMesh.GetComponent<TextMeshProUGUI>().text = $"{waveInfo.Count}";
        
            if (_currentEnemyNameTextMesh != null) 
                _currentEnemyNameTextMesh.GetComponent<TextMeshProUGUI>().text = $"{waveInfo.EnemyName}";

            if(_currentEnemyImage != null) {
                switch (waveInfo.EnemyName)
                {
                    case "Orc":
                    case "Berserker":
                        _currentEnemyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Orc");
                        break;
                    case "Eagle":
                    case "Great Eagle":
                        _currentEnemyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Eagle");
                        break;
                        
                    case "Deathlord":
                        _currentEnemyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Troll");
                        break;
                        
                    case "Skeleton":
                    case "Skeleton Lord":
                        _currentEnemyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Skeleton");
                        break;
                        
                    default:
                        _currentEnemyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Skeleton");
                        break;
                }
            }
            
        }
        
    }

     private void UpdateCurrentEnemies(float lifetime) {
        if (_enemiesTextMesh != null) {
            int _totalEnemies = GameManager.instance.TotalEnemies;
            int _currentEnemies = GameManager.instance.CurrentEnemies;

            _enemiesTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_currentEnemies} / {_totalEnemies}";
        }
    }

    private void DisplayWaveEndStats() {
        GameManager.instance.PauseGame(true);
        
        // Show menu

        if (_showingMenu)
            return;
        
        _showingMenu = true;

        GameObject menu = GameObject.Find("End wave stats");
        menu.transform.Translate(600 * Vector3.up);

        print("Showing menu");

        // Load stats

        float _avgEnemyLifetime = GameManager.instance.AvgEnemyLifetime;
        _avgEnemyLifeTimeTextMesh.GetComponent<TextMeshProUGUI>().text = $"{Math.Round(_avgEnemyLifetime, 2)} s";

        float _avgTurretLifetime = GameManager.instance.AvgTurretLifetime;
        _avgTurretLifeTimeTextMesh.GetComponent<TextMeshProUGUI>().text = $"{Math.Round(_avgTurretLifetime, 2)} s";
    }

    public void UpdateTimer(int countdown)
    {
        if(_timerTextMesh != null) {
            _timerTextMesh.GetComponent<TextMeshProUGUI>().text = $"{countdown}";
            int seconds = Int32.Parse(_timerTextMesh.GetComponent<TextMeshProUGUI>().text);
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
            _timerCoroutine = StartCoroutine(UpdateTimerCoroutine(seconds));
        }
    }
    
    private IEnumerator UpdateTimerCoroutine(int seconds)
    {
        while (seconds > 0)
        {
            _timerTextMesh.GetComponent<TextMeshProUGUI>().text = $"{seconds}";
            yield return new WaitForSeconds(1f);
            seconds--;
        }

        _timerTextMesh.GetComponent<TextMeshProUGUI>().text = "0";
    }
}