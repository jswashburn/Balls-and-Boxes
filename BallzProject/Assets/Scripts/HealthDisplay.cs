using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [Range(0, 4)][SerializeField] byte _depth;

    public static string HealthDisplayed { get; set; }
    public byte Depth { get; private set; }

    public void ChangeColor(Color32 color)
    {
        _tmpText.color = color;
    }

    void Awake()
    {
        Depth = _depth;
    }

    void Update()
    {
        _tmpText.text = HealthDisplayed;
    }
}
