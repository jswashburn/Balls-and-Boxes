using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Ball : MonoBehaviour, IColorChangeable
{
    // Git test comment
    [SerializeField] float _respawnDelay;
    [SerializeField] bool _primary;

    public bool Primary { get; set; }

    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    Vector3 _startingPosition;
    Vector3 _deadBallArea;
    bool _collidedWithBox;

    public void ChangeColor(string theme)
    {
        var newColor = new ColorThemes(Primary).Colors[theme];
        _spriteRenderer.color = newColor;
    }

    void Awake()
    {
        Primary = _primary;
        _rb = GetComponent<Rigidbody2D>();
        _startingPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _deadBallArea = new Vector3(-5, -5, 0f);
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
        _collidedWithBox = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Box>())
        {
            _collidedWithBox = true;
            StartCoroutine(DoAfterDelay(Reset, _respawnDelay));
        }
        else if (!_collidedWithBox)
        {
            StartCoroutine(DoAfterDelay(Die, _respawnDelay));
        }
    }

    void Die()
    {
        if (!_collidedWithBox) transform.position = _deadBallArea;
        Reset(_deadBallArea);
    }

    public void Reset(Vector3 position)
    {
        _rb.gravityScale = 0f;

        transform.position = position;
        _rb.velocity = Vector3.zero;
    }

    IEnumerator DoAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
        _collidedWithBox = false;
    }
}