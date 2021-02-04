using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region Fields and Properties
    
    [SerializeField] int _defaultStartingHealth;
    [SerializeField] GameObject _boxPrefab;

    int CurrentHealth { get; set; }

    byte _currentTheme = 0;
    float _playTime;
    float _difficultyIncreaseTimer;
    bool _paused = false;

    List<IColorChangeable> _colorChangeableObjects;
    MonoBehaviour[] _sceneObjects;
    ColorThemes _colors;

    #endregion

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
        _colors = new ColorThemes();
    }

    void Start()
    {
        CurrentHealth = _defaultStartingHealth;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
    }

    void Update()
    {
        PlayTimer();
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

    #region Difficulty Update

    void UpdateDifficulty()
    {
        _difficultyIncreaseTimer += Time.deltaTime;

        // Every 15 seconds, "update the difficulty"
        if (_difficultyIncreaseTimer > 15f)
        {
            _difficultyIncreaseTimer = 0f;
            Instantiate(_boxPrefab, Vector3.zero, Quaternion.identity);
            BallBelt.RestoreBall();
            NextColorTheme();
        }
    }

    void PlayTimer()
    {
        // Display time in seconds since start of the game
        _playTime += Time.deltaTime;
        PlayTimeDisplay.PlayTimeDisplayed = Convert.ToInt32(_playTime).ToString();
    }

    void NextColorTheme()
    {
        RefreshColorChangeables();

        // Have each color changeable object change to its next appropriate color
        foreach (var colorChangeable in _colorChangeableObjects)
            colorChangeable.ChangeColor(
                _colors.Colors[colorChangeable.Level][_currentTheme]
                );

        Camera.main.backgroundColor = _colors.Colors[0][_currentTheme];

        _currentTheme++;
        _currentTheme %= (byte)(_colors.Colors[0].Count);
    }
    
    void RefreshColorChangeables()
    {
        _sceneObjects = FindObjectsOfType<MonoBehaviour>();

        // Get all the objects in the scene that can change colors
        foreach (var obj in _sceneObjects)
        {
            if (obj is IColorChangeable)
                _colorChangeableObjects.Add((IColorChangeable)obj);
        }
    }

    #endregion
    
    void TakeHit()
    {
        CurrentHealth--;
        HealthDisplay.HealthDisplayed = CurrentHealth.ToString();
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
