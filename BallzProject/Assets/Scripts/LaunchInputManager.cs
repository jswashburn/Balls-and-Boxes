using UnityEngine;

public class LaunchInputManager : MonoBehaviour, IColorChangeable
{
    [SerializeField] LineRendererController _lineRendererController;
    [SerializeField] float _launchForce;
    [SerializeField] float _gravityScale;
    [SerializeField] float _maxLineLength;
    [SerializeField] SpriteRenderer _clickAreaSpriteRenderer;
    [Range(0, 4)][SerializeField] byte _depth;

    Ball _ball;
    Rigidbody2D _rb;
    Vector3 _clickedPosition, _direction, _trajectory, _currentMousePosition;
    Camera _mainCamera;
    Color _clickAreaStartColor;

    public byte Depth { get; private set; }

    public void ChangeColor(Color32 color)
    {
        _clickAreaStartColor = new Color32(color.r, color.g, color.b, 0);
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        Depth = _depth;
        _ball = GetComponent<Ball>();
    }

    void OnMouseDown()
    {
        if (_ball.WasLaunched) return;

        _clickAreaSpriteRenderer.color = new Color(
            _clickAreaStartColor.r, _clickAreaStartColor.g, _clickAreaStartColor.b,
            0.2f
        );

        if (!PauseMenu.Paused)
            _clickedPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (_ball.WasLaunched) return;

        if (!PauseMenu.Paused)
        {
            _currentMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _trajectory = (_clickedPosition - _currentMousePosition) + _clickedPosition;

            var currentPosition = transform.position;
            var lineRendererEnd = new Vector3 (_trajectory.x, 
                Mathf.Clamp(_trajectory.y, currentPosition.y, _maxLineLength));

            _lineRendererController.DrawLine(currentPosition, lineRendererEnd);
        }
    }

    void OnMouseUp()
    {
        if (_ball.WasLaunched) return;

        _clickAreaSpriteRenderer.color = _clickAreaStartColor;
        if (!PauseMenu.Paused)
        {
            _lineRendererController.ClearLine();
            
            // Add force in the direction of the trajectory, at the position of wherever our line starts (ball)
            var currentPosition = transform.position;
            _rb.AddForceAtPosition((_trajectory - currentPosition).normalized * _launchForce,
                currentPosition);
            _rb.gravityScale = _gravityScale;

            _ball.WasLaunched = true;
        } 
    }
}
