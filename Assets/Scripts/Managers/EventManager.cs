using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    static public EventManager instance;

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public event Action<bool> OnGameOver;
    public event Action<int> OnLivesChange;
    public event Action<int> OnCoinChange;
    public event Action<int, int, WaveManager.Wave> OnWaveChange;
    public event Action<float> OnEnemyDeath;
    public event Action<float> OnTowerDestroyed;
    public event Action<GameObject> OnAttack;

    public event Action OnBossWave;

    public void GameOver(bool isVictory)  {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    public void LivesChange(int livesChange) {
        if (OnLivesChange != null) OnLivesChange(livesChange);
    }

    public void CoinChange(int coinChange) {
        if (OnCoinChange != null) OnCoinChange(coinChange);
    }

    public void WaveChange(int currentWave, int maxWave, WaveManager.Wave waveInfo) {
        if (OnWaveChange != null) OnWaveChange(currentWave, maxWave, waveInfo);
    }

    public void EnemyDeath(float lifetime) {
        if (OnEnemyDeath != null) OnEnemyDeath(lifetime);
    }

    public void TowerDestroyed(float lifetime) {
        if (OnTowerDestroyed != null) OnTowerDestroyed(lifetime);
    }

    public void Attack(GameObject attacker) {
        if (OnAttack != null) OnAttack(attacker);
    }

    public void BossWave()
    {
        if (OnBossWave != null) OnBossWave();
    }
}