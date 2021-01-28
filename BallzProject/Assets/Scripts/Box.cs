using UnityEngine;

public class Box : MonoBehaviour, IColorChangeable
{
    [SerializeField] int _boxHealth;
    [SerializeField] float _fallSpeed;
    [SerializeField] float _xMin, _xMax, _yMin, _yMax;
    [SerializeField] bool _primary;
    
    public bool Primary { get; set; }
    int HealthPoints { get; set; }
    
    SpriteRenderer _spriteRenderer;

    public void ChangeColor(string theme)
    {
        var newColor = new ColorThemes(Primary).Colors[theme];
        _spriteRenderer.color = newColor;
    }
    
    public void Die()
    {
        transform.position = new Vector3(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax), 0f);

        HealthPoints = _boxHealth;
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Primary = _primary;
    }

    void Start()
    {
        transform.position = new Vector3(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax), 0f);
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    void Update()
    {
        transform.position += Time.deltaTime * _fallSpeed * Vector3.down;

        if (HealthPoints <= 0)
            Die();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
            HealthPoints--;
    }
}
