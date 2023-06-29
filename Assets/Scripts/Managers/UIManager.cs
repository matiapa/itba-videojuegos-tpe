using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject _coinsTextMesh;
    [SerializeField] private GameObject _livesTextMesh;
    [SerializeField] private GameObject _waveTextMesh;
    
    [SerializeField] private GameObject _basicTurretTextMesh;
    [SerializeField] private GameObject _iceTurretTextMesh;
    [SerializeField] private GameObject _poisonTurretTextMesh;
    private int[] _turretsCost;

    private void Start() {
        GameManager.instance.OnNetCoinChange += UpdateCoinsValue;
        GameManager.instance.OnNetLivesChange += UpdateLivesValue;
        EventManager.instance.OnWaveChange += UpdateCurrentWave;
        
        _turretsCost = BuildingSystem.instance.EntitiesCost;
        _basicTurretTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[0]}";
        _iceTurretTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[1]}";
        _poisonTurretTextMesh.GetComponent<TextMeshProUGUI>().text = $"{_turretsCost[2]}";
        
        UpdateCoinsValue(GameManager.instance.Coins);
        UpdateLivesValue(GameManager.instance.Lives);
        UpdateCurrentWave(GameManager.instance.CurrentWave, GameManager.instance.MaxWave);
    }

    private void UpdateCoinsValue(int newCoins) {
        if (_coinsTextMesh != null) 
            _coinsTextMesh.GetComponent<TextMeshProUGUI>().text = $"{newCoins}";
    }

     private void UpdateLivesValue(int newLives) {
        if (_livesTextMesh != null)
            _livesTextMesh.GetComponent<TextMeshProUGUI>().text = $"{newLives}";
    }

    private void UpdateCurrentWave(int currentWave, int maxWave) {
        if (_waveTextMesh != null)
            _waveTextMesh.GetComponent<TextMeshProUGUI>().text = $"{currentWave} / {maxWave}";
    }
}