using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Color _currentColour;
    private Color _desiredColour;

    [SerializeField] private Color baseColour = Color.white;
    [SerializeField] private Color _temporaryColour;
    [SerializeField] private float _colorChangeSpeed;
    [SerializeField] private bool _snap;
    private float _tempColourEndTime;

    //stretch goal

    //public void LerpColour(Color c, float sp)
    //{
      
    //}
    //public void LerpColour(Color c, float sp, float time)
    //{
       
    //}
    public void SetBaseColour(Color c)
    {
        baseColour = c;
    }
    public void SetTemporaryColor(Color c, float time)
    {
        _temporaryColour = c;
        _tempColourEndTime = Time.time + time;
    }
    private void Update()
    {
        if(Time.time < _tempColourEndTime)
        {
            _desiredColour = _temporaryColour;
            _currentColour = _temporaryColour;
        }
        else
        {
            _desiredColour = baseColour;
        }

        _currentColour = Color.Lerp(_currentColour, _desiredColour, 5f * Time.deltaTime);
    }
}
