using UnityEngine;

public class LaunchInputManager : MonoBehaviour
{
    [SerializeField] LineRendererController _lineRendererController;
    [SerializeField] float _launchForce;
    [SerializeField] float _gravityScale;
    [SerializeField] float _maxLineLength;

    Rigidbody2D _rb;
    Vector3 _clickedPosition;
    Vector3 _direction;
    Vector3 _trajectory;
    Vector3 _currentMousePosition;
    Camera _mainCamera;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        _clickedPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        _currentMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _trajectory = (_clickedPosition - _currentMousePosition) + _clickedPosition;

        var currentPosition = transform.position;
        _lineRendererController.DrawLine(currentPosition, 
            new Vector3 (_trajectory.x, Mathf.Clamp(_trajectory.y, currentPosition.y, _maxLineLength)));
    }

    void OnMouseUp()
    {
        _lineRendererController.ClearLine();
        
        // Add force in the direction of the trajectory, at the position of wherever our line starts (ball)
        var currentPosition = transform.position;
        _rb.AddForceAtPosition((_trajectory - currentPosition).normalized * _launchForce,
            currentPosition);
        _rb.gravityScale = _gravityScale;
    }
}
