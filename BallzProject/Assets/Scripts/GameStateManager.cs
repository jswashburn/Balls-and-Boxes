using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] int _defaultStartingHealth;
    [SerializeField] GameObject _boxPrefab;

    float _playTime;
    float _difficultyIncreaseTimer;

    int CurrentHealth { get; set; }

    public delegate void GameStateManagerAction();
    public static event GameStateManagerAction OnHealthBelowZero;
    public static event GameStateManagerAction OnDifficultyIncrease;

    void OnEnable()
    {
        DamageArea.OnBoxEnteredDamageArea += TakeHit;
    }

    void OnDisable()
    {
        DamageArea.OnBoxEnteredDamageArea -= TakeHit;
    }

    void Start()
    {
        CurrentHealth = _defaultStartingHealth;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
    }

    void Update()
    {
        RecordPlayTime();
        UpdateDifficulty();

        if (CurrentHealth <= 0)
            OnHealthBelowZero?.Invoke(); // TODO: Game over
    }

    void UpdateDifficulty()
    {
        _difficultyIncreaseTimer += Time.deltaTime;

        // Every 15 seconds, trigger a difficulty update
        if (_difficultyIncreaseTimer > 15f)
        {
            _difficultyIncreaseTimer = 0f;
            OnDifficultyIncrease?.Invoke();
        }
    }

    void RecordPlayTime()
    {
        // Display time in seconds since start of the game
        _playTime += Time.deltaTime;
        PlayTimeDisplay.PlayTimeDisplayed = Convert.ToInt32(_playTime).ToString();
    }
    
    void TakeHit()
    {
        CurrentHealth--;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
    }
}
