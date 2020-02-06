using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSprite : MonoBehaviour
{
    //[SerializeField] internal bool changingColour;

    private SpriteRenderer _sprite;
    private ColorController _controller;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _controller = GameObject.FindObjectOfType<ColorController>();

        Update();
    }
    private void Update()
    {
        if(_sprite != null && _controller != null)
        {
            _sprite.color = _controller._currentColour;
        }
    }
}
