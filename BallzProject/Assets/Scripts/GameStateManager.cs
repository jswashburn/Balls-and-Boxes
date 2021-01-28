using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] int _defaultStartingHealth;
    [SerializeField] GameObject _boxPrefab;

    int CurrentHealth { get; set; }
    int CurrentColorThemeCode { get; set; }

    float _playTime;
    float _difficultyIncreaseTimer;

    List<IColorChangeable> _colorChangeableObjects;
    MonoBehaviour[] _sceneObjects;

    public delegate void GameStateManagerAction();
    public static event GameStateManagerAction OnHealthBelowZero;

    void OnEnable()
    {
        DamageArea.OnBoxEnteredDamageArea += TakeHit;
    }

    void OnDisable()
    {
        DamageArea.OnBoxEnteredDamageArea -= TakeHit;
    }

    void Awake()
    {
        CurrentColorThemeCode = 0;
        _colorChangeableObjects = new List<IColorChangeable>();
    }

    void UpdateColorChangeables()
    {
        _sceneObjects = FindObjectsOfType<MonoBehaviour>();

        // Get all the objects that can change colors
        foreach (var monoBehavior in _sceneObjects)
        {
            var colorChangeable = monoBehavior.GetComponent<IColorChangeable>();
            if (colorChangeable != null)
            {
                _colorChangeableObjects.Add(colorChangeable);
            }
        }
    }

    void Start()
    {
        CurrentHealth = _defaultStartingHealth;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
    }

    void Update()
    {
        _playTime += Time.deltaTime;
        _difficultyIncreaseTimer += Time.deltaTime;
        
        PlayTimeDisplay.PlayTimeDisplayed = Convert.ToInt32(_playTime).ToString();

        if (_difficultyIncreaseTimer > 15f)
        {
            _difficultyIncreaseTimer = 0f;
            Instantiate(_boxPrefab, Vector3.zero, Quaternion.identity);
            NextColorTheme();
        }
        
        if (CurrentHealth <= 0)
            OnHealthBelowZero?.Invoke(); // TODO: Game over
    }

    void TakeHit()
    {
        CurrentHealth--;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
    }

    void NextColorTheme()
    {
        UpdateColorChangeables();
        
        CurrentColorThemeCode = (CurrentColorThemeCode + 1) % ThemeNames.Names.Count;

        var nextTheme = ThemeNames.Names[CurrentColorThemeCode];
        foreach (var colorChangeable in _colorChangeableObjects)
        {
            colorChangeable.ChangeColor(nextTheme);
        }

        Camera.main.backgroundColor = new ColorThemes(false).Colors[nextTheme]; 
    }
}
