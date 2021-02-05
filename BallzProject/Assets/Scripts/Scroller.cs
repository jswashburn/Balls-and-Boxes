using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour, IColorChangeable
{
    [SerializeField] float _scrollSpeed;
    [Range(0, 4)][SerializeField] byte _level;
    
    Vector3 _startingPosition;
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
