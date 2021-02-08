using UnityEngine;

public class Box : MonoBehaviour, IColorChangeable
{
    [SerializeField] int _boxHealth;
    [SerializeField] float _fallSpeed;
    [SerializeField] float _maxFallSpeed;
    [SerializeField] float _xMin, _xMax, _yMin, _yMax;
    [Range(0, 4)][SerializeField] byte _depth;
    
    int _health;
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
        SetRandomPosition();
        SetRandomSpeed();
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    void Update()
    {
        transform.position += Time.deltaTime * _fallSpeed * Vector3.down;

        if (_health <= 0)
            Die();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
            _health--;
    }

    public void Die()
    {
        SetRandomPosition();
        SetRandomSpeed();
        _health = _boxHealth;
    }

    void SetRandomPosition()
    {
        transform.position = new Vector3(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax), 0f);
    }

    void SetRandomSpeed()
    {
        _fallSpeed = Random.Range(1f, _maxFallSpeed);
    }
}
