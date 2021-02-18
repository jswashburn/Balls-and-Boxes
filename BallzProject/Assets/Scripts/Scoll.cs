using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoll : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _speed;

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.material.mainTextureOffset += Vector2.left * Time.deltaTime * _speed;    
    }
}
