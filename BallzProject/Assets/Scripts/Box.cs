using UnityEngine;

public class Box : MonoBehaviour, IColorChangeable
{
    [SerializeField] int _boxHealth;
    [SerializeField] float _fallSpeed;
    [SerializeField] float _xMin, _xMax, _yMin, _yMax;
    [Range(0, 4)][SerializeField] byte _level;
    
    int _health;
    SpriteRenderer _spriteRenderer;

    public byte Level { get; private set; }

    public void ChangeColor(Color32 color)
    {
        _spriteRenderer.color = color;
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Level = _level;
    }

    void Start()
    {
        transform.position = new Vector3(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax), 0f);
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
        transform.position = new Vector3(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax), 0f);

        _health = _boxHealth;
    }
}
