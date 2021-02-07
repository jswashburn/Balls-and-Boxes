using System.Collections.Generic;
using UnityEngine;

public class ColorThemeChanger : MonoBehaviour
{
    [SerializeField] GameObject _prefab;

    public static List<IColorChangeable> ColorChangeables;

    ColorThemes _colors;
    byte _currentTheme = 0;

    void OnEnable()
    {
        GameStateManager.OnDifficultyIncrease += NextColorTheme;
    }

    void OnDisable()
    {
        GameStateManager.OnDifficultyIncrease -= NextColorTheme;
    }

    void Awake()
    {
        ColorChangeables = new List<IColorChangeable>();
        _colors = new ColorThemes();
    }

    void Start()
    {
        NextColorTheme();
    }

    void NextColorTheme()
    {
        Instantiate(_prefab, Vector3.zero, Quaternion.identity);

        RefreshColorChangeables();

        // Have each color changeable object change to its next appropriate color
        foreach (var obj in ColorChangeables)
        {
            obj.ChangeColor(_colors.Colors[obj.Depth][_currentTheme]);
        }
            
        Camera.main.backgroundColor = _colors.Colors[0][_currentTheme];

        _currentTheme++;
        _currentTheme %= (byte)(_colors.Colors[0].Count);
    }

    void RefreshColorChangeables()
    {
        foreach (var obj in FindObjectsOfType<MonoBehaviour>())
        {
            if (obj is IColorChangeable)
                ColorChangeables.Add((IColorChangeable)obj);
        }
    }
}
