using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [SerializeField] bool _primary;

    public static string HealthDisplayed { get; set; }
    public bool Primary { get; set; }

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
        _tmpText.text = HealthDisplayed;
    }
}
