using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour, IColorChangeable
{
    [SerializeField] float _scrollSpeed;
    [SerializeField] bool _primary;

    public bool Primary { get; set; }
    
    Vector3 _startingPosition;
    SpriteRenderer _spriteRenderer;

    public void ChangeColor(int theme)
    {
        _spriteRenderer.color = new ColorThemes(Primary).Colors[theme];
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Primary = _primary;
    }

    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.x <= 0f)
            transform.position = _startingPosition;
        else
            transform.position += Time.deltaTime * _scrollSpeed * Vector3.left;
    }
}
