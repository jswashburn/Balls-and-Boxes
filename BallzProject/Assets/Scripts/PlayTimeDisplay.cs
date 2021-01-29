using System;
using UnityEngine;
using TMPro;

public class PlayTimeDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [SerializeField] bool _primary;

    public bool Primary { get; set; }
    public static string PlayTimeDisplayed { get; set; }
    
    public void ChangeColor(int theme)
    {
        _tmpText.color = new ColorThemes(Primary).Colors[theme];
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
