using System;
using UnityEngine;
using TMPro;

public class PlayTimeDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [SerializeField] bool _primary;

    public bool Primary { get; set; }
    public static string PlayTimeDisplayed { get; set; }
    
    public void ChangeColor(string theme)
    {
        var newColor = new ColorThemes(Primary).Colors[theme];
        _tmpText.color = newColor;
    }

    void Awake()
    {
        Primary = _primary;
    }

    void Update()
    {
        _tmpText.text = PlayTimeDisplayed;
    }
}
