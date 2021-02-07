using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererController : MonoBehaviour, IColorChangeable
{
    [Range(0, 4)][SerializeField] byte _depth;

    ColorThemes _colorThemes;
    LineRenderer _lineRenderer;

    public byte Depth { get; private set; }
    
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Depth = _depth;
    }

    void Start()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.material.mainTextureScale = new Vector2(1f / _lineRenderer.startWidth, 1.0f);
    }

    public void ChangeColor(Color32 color)
    {    
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = new Color32(color.r, color.g, color.b, 0);
    }
    
    public void DrawLine(Vector3 start, Vector3 end)
    {
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
    }

    public void ClearLine()
    {
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);
    }
}
