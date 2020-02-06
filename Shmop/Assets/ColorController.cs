using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Color _colour;
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
            _colour = _temporaryColour;
        }
        else
        {
            _colour = baseColour;
        }
    }
}
