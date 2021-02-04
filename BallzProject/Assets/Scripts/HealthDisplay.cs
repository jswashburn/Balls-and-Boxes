using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [SerializeField] byte _level;

    public static string HealthDisplayed { get; set; }
    public byte Level { get; private set; }

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
        _tmpText.text = HealthDisplayed;
    }
}
