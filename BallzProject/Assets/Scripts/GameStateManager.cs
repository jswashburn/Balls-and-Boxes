using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] int _defaultStartingHealth;
    [SerializeField] GameObject _boxPrefab;

    int CurrentHealth { get; set; }

    int _currentColorThemeCode = 0;
    float _playTime;
    float _difficultyIncreaseTimer;
    bool _paused;

    List<IColorChangeable> _colorChangeableObjects;
    MonoBehaviour[] _sceneObjects;

    #region Events

    public delegate void GameStateManagerAction();
    public static event GameStateManagerAction OnHealthBelowZero;

    #endregion

    #region UnityEvents

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
        _colorChangeableObjects = new List<IColorChangeable>();
    }

    void Start()
    {
        CurrentHealth = _defaultStartingHealth;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
        _paused = false;
    }

    void Update()
    {
        UpdateDifficulty();

        if (CurrentHealth <= 0)
            OnHealthBelowZero?.Invoke(); // TODO: Game over

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_paused)
                Pause();
            else
                Resume();
        }
    }

    #endregion

    void UpdateDifficulty()
    {
        _playTime += Time.deltaTime;
        _difficultyIncreaseTimer += Time.deltaTime;

        PlayTimeDisplay.PlayTimeDisplayed = Convert.ToInt32(_playTime).ToString();

        if (_difficultyIncreaseTimer > 15f)
        {
            _difficultyIncreaseTimer = 0f;
            Instantiate(_boxPrefab, Vector3.zero, Quaternion.identity);
            BallBelt.RestoreBall();
            NextColorTheme();
        }
    }

    void TakeHit()
    {
        CurrentHealth--;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
    }

    void NextColorTheme()
    {
        UpdateColorChangeables();
        
        _currentColorThemeCode = (_currentColorThemeCode + 1) % ColorThemes.numThemes;

        foreach (var colorChangeable in _colorChangeableObjects)
        {
            colorChangeable.ChangeColor(_currentColorThemeCode);
        }

        Camera.main.backgroundColor = new ColorThemes(false).Colors[_currentColorThemeCode]; 
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

    void Pause()
    {
        _paused = true;
        Time.timeScale = 0f;
    }

    void Resume()
    {
        _paused = false;
        Time.timeScale = 1f;
    }
}
