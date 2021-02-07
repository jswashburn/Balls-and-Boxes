using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour, IColorChangeable
{
    [SerializeField] float _respawnDelay;
    [Range(0, 4)][SerializeField] byte _depth;

    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    Vector3 _startingPosition;
    bool _collidedWithBox;

    public byte Depth { get; private set; }

    public void ChangeColor(Color32 color)
    {
        _spriteRenderer.color = color;
    }

    void Awake()
    {
        Depth = _depth;
        _rb = GetComponent<Rigidbody2D>();
        _startingPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
        _collidedWithBox = false;
    }

    // On launch if you dont hit a box by _respawnDelay seconds, ball 'dies'
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
        if (!_collidedWithBox)
        {
            gameObject.SetActive(false);
            BallBelt.EnqueueDeadBall(this);
        }
    }

    public void Reset()
    {
        _rb.gravityScale = 0f;

        transform.position = _startingPosition;
        _rb.velocity = Vector3.zero;
    }

    IEnumerator DoAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
        _collidedWithBox = false;
    }
}