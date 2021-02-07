using UnityEngine;
using TMPro;

public class PlayTimeDisplay : MonoBehaviour, IColorChangeable
{
    [SerializeField] TextMeshProUGUI _tmpText;
    [Range(0, 4)][SerializeField] byte _depth;

    public byte Depth { get; private set; }
    public static string PlayTimeDisplayed { get; set; }
    
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
        _tmpText.text = PlayTimeDisplayed;
    }
}
