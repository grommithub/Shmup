using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hyperdrive : MonoBehaviour
{
    [SerializeField] private UnityEvent _startHyperDrive;
    private Vector3 _middle = new Vector3();
    private bool _hyperDriving;

    private bool _centering;
    private float _distance;

    [SerializeField] private float _hyperDrivespeed = 30f;
    [SerializeField] private float _goToMiddleSpeed = 3f;
    [SerializeField] private float _stretch = 1.25f;
    private void Start()
    {
        _middle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/3, Screen.height/2, 0f));
        _middle.z = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && !_centering && !_hyperDriving)
        {
            _hyperDriving = true;
            _centering = true;
             
        }
        if (!_hyperDriving) return;
        if (_centering)
        {
            if ((transform.position - _middle).magnitude < 0.05f)
            {
                _centering = false;
                _startHyperDrive.Invoke();
            }
        transform.position = Vector3.Lerp(transform.position, _middle, _goToMiddleSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += (Vector3.right * _hyperDrivespeed * Time.deltaTime);
            transform.localScale = new Vector3(_stretch, 1f, 1f);
        }
    }
}
