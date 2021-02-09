using UnityEngine;

public class LaunchInputManager : MonoBehaviour, IColorChangeable
{
    [SerializeField] float _maxLineLength;
    [SerializeField] float _launchForce;
    [SerializeField] float _gravityScale;
    [SerializeField] LineRendererController _lineRendererController;
    [SerializeField] SpriteRenderer _circleAroundBall;
    [Range(0, 4)][SerializeField] byte _depth;

    Ball _ball;
    BoxCollider2D _clickableArea;
    Rigidbody2D _rb;
    Vector3 _clickedPosition, _trajectory, _currentMousePosition, _draggedDirection, _draggedFromBall;

    Camera _mainCamera;

    Color _circleAroundBallStartColor;
    public byte Depth { get; private set; }

    public void ChangeColor(Color32 color)
    {
        _circleAroundBallStartColor = new Color32(color.r, color.g, color.b, 0);
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _clickableArea = GetComponent<BoxCollider2D>();
        _mainCamera = Camera.main;
        Depth = _depth;
        _ball = GetComponent<Ball>();
    }

    bool ControlsEnabled() => !PauseMenu.Paused && !_ball.WasLaunched;
    Vector3 GetForceVector() => _trajectory - transform.position;

    void OnMouseDown()
    {
        if (ControlsEnabled())
        {
            HighlightCircleAroundBall();
            _clickedPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseDrag()
    {
        if (ControlsEnabled())
            DrawLine();
    }

    void OnMouseUp()
    {
        if (ControlsEnabled())
        {
            Launch();
            _ball.WasLaunched = true;
            _clickableArea.enabled = false;
        }
    } 

    void Launch()
    {
        _circleAroundBall.color = _circleAroundBallStartColor;
        _lineRendererController.ClearLine();

        _rb.gravityScale = _gravityScale;
        _rb.AddForceAtPosition(GetForceVector().normalized * _launchForce, transform.position);
    }

    void DrawLine()
    {
        CalculateTrajectory();
        _lineRendererController.DrawLine(transform.position, _trajectory);
    }

    void CalculateTrajectory()
    {
        _currentMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _draggedDirection = _clickedPosition - _currentMousePosition;
        _draggedFromBall = transform.position + _draggedDirection;
        _trajectory = Vector3.Reflect(new Vector3(_draggedFromBall.x, -_draggedFromBall.y, 0), Vector3.up);
    }

    void HighlightCircleAroundBall()
    {
        _circleAroundBall.color = new Color(
            _circleAroundBallStartColor.r, _circleAroundBallStartColor.g, _circleAroundBallStartColor.b, 0.2f);
    }
}
