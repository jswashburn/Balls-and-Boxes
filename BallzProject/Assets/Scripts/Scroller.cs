using UnityEngine;

public class Scroller : MonoBehaviour, IColorChangeable
{
    [SerializeField] float _scrollSpeed;
    [SerializeField] float _xResetPoint;
    [Range(0, 4)][SerializeField] byte _depth;
    
    Vector3 _startingPosition;
    SpriteRenderer _spriteRenderer;

    public byte Depth { get; private set; }

    public void ChangeColor(Color32 color)
    {
        _spriteRenderer.color = color;
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Depth = _depth;
    }

    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        ScrollLeft();
    }

    void ScrollLeft()
    {
        if (transform.position.x <= _xResetPoint)
            transform.position = _startingPosition;
        else
            transform.position += Time.deltaTime * _scrollSpeed * Vector3.left;
    }
}
