using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [SerializeField] bool _primary;

    public static string HealthDisplayed { get; set; }
    public bool Primary { get; set; }

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
        _tmpText.text = HealthDisplayed;
    }
}
