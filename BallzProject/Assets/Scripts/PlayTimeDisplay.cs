using System;
using UnityEngine;
using TMPro;

public class PlayTimeDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [Range(0, 4)][SerializeField] byte _level;

    public byte Level { get; private set; }
    public static string PlayTimeDisplayed { get; set; }
    
    public void ChangeColor(Color32 color)
    {
        _tmpText.color = color;
    }

    void Awake()
    {
        Level = _level;
    }

    void Update()
    {
        _tmpText.text = PlayTimeDisplayed;
    }
}
