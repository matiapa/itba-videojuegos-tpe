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
    public event Action<int, int> OnWaveChange;
    public event Action<GameObject> OnAttack;

    public void GameOver(bool isVictory)  {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    public void LivesChange(int livesChange) {
        if (OnLivesChange != null) OnLivesChange(livesChange);
    }

    public void CoinChange(int coinChange) {
        if (OnCoinChange != null) OnCoinChange(coinChange);
    }

    public void WaveChange(int currentWave, int maxWave) {
        if (OnWaveChange != null) OnWaveChange(currentWave, maxWave);
    }

    public void Attack(GameObject attacker) {
        if (OnAttack != null) OnAttack(attacker);
    }
}