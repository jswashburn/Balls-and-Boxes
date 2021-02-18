using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] int _defaultStartingHealth;
    [SerializeField] GameObject _boxPrefab;

    public float survivedTime = 0;
    public float difficultyIncreaseTimer = 0;

    public static int CurrentHealth { get; set; }

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
        survivedTime += Time.deltaTime;
        difficultyIncreaseTimer += Time.deltaTime;
        UpdateDifficulty();

        if (CurrentHealth <= 0)
            OnHealthBelowZero?.Invoke();
    }

    void UpdateDifficulty()
    {
        if (difficultyIncreaseTimer > 15f)
        {
            difficultyIncreaseTimer = 0f;
            OnDifficultyIncrease?.Invoke();
        }
    }
    
    void TakeHit()
    {
        CurrentHealth--;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
    }
}
