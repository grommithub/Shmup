using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSprite : MonoBehaviour
{

    internal bool _hasOwnColour;
    [SerializeField] private Color _ownColour;
    private float _changeBackTime;

    private SpriteRenderer _sprite;
    private Image _img;
    private ColorController _controller;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _controller = GameObject.FindObjectOfType<ColorController>();

        if(_sprite == null)
        _img = GetComponent<Image>();

        Update();
    }
    private void Update()
    {
        if(_img != null)
        {
            if (_controller != null && !_hasOwnColour)
            {
                _img.color = _controller._currentColour;
            }
            else
            {
                _img.color = _ownColour;
                if (Time.time > _changeBackTime) _hasOwnColour = false;
            }
            return;
        }

        if(_sprite != null && _controller != null && !_hasOwnColour)
        {
            _sprite.color = _controller._currentColour;
        }
        else
        {
            _sprite.color = _ownColour;
            if (Time.time > _changeBackTime) _hasOwnColour = false;
        }
    }

    public void ChangeColour(Color colour, float duration)
    {
        _hasOwnColour = true;
        _ownColour = colour;
        _changeBackTime = Time.time + duration;
    }
}

//public class ColorController : MonoBehaviour
//{
//    public Color _currentColour;
//    private Color _desiredColour;

//    [SerializeField] private Color baseColour = Color.white;
//    [SerializeField] private Color _temporaryColour;
//    [SerializeField] private float _colorChangeSpeed;
//    [SerializeField] private bool _snap;
//    private float _tempColourEndTime;

//    //stretch goal

//    //public void LerpColour(Color c, float sp)
//    //{

//    //}
//    //public void LerpColour(Color c, float sp, float time)
//    //{

//    //}
//    public void SetBaseColour(Color c)
//    {
//        baseColour = c;
//    }
//    public void SetTemporaryColor(Color c, float time)
//    {
//        _temporaryColour = c;
//        _tempColourEndTime = Time.time + time;
//    }
//    private void Update()
//    {
//        if (Time.time < _tempColourEndTime)
//        {
//            _desiredColour = _temporaryColour;
//            _currentColour = _temporaryColour;
//        }
//        else
//        {
//            _desiredColour = baseColour;
//        }

//        _currentColour = Color.Lerp(_currentColour, _desiredColour, 5f * Time.deltaTime);
//    }
//}
