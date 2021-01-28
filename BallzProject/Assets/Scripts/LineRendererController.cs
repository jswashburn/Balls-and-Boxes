using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererController : MonoBehaviour, IColorChangeable
{
    LineRenderer _lineRenderer;
    [SerializeField] bool _primary;

    public bool Primary { get; set; }
    
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Primary = _primary;
    }

    void Start()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.material.mainTextureScale = new Vector2(1f / _lineRenderer.startWidth, 1.0f);
    }

    public void ChangeColor(string theme)
    {
        var newColor = new ColorThemes(Primary).Colors[theme];
        _lineRenderer.startColor = newColor;
        _lineRenderer.endColor = new Color32(newColor.r, newColor.g, newColor.b, 0);
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
