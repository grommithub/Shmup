using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hyperdrive : MonoBehaviour
{
    [SerializeField] private UnityEvent _startHyperDrive;
    [SerializeField] private UnityEvent _startCentering;
    
    [SerializeField] private float _centeringTime = 5f;
    [SerializeField] private float _hyperDrivespeed = 30f;
    
    private bool _hyperDriving;
    private bool _centering;
<<<<<<< Updated upstream
    private float _distance;

    [SerializeField] private float _hyperDrivespeed = 30f;
    [SerializeField] private float _goToMiddleSpeed = 3f;
    [SerializeField] private float _stretch = 1.25f;
=======

    private Vector2 _middle = new Vector2();
    private Vector2 _startOffset = new Vector2();
    
    private float _centerStartTime;

>>>>>>> Stashed changes
    private void Start()
    {
        _middle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/3, Screen.height/2, 0f));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && !_centering && !_hyperDriving)
        {
<<<<<<< Updated upstream
=======
            _startOffset = (Vector2)transform.position - _middle;
            _centerStartTime = Time.time;

            _startCentering.Invoke();
>>>>>>> Stashed changes
            _hyperDriving = true;
            _centering = true;
        }
        
        if (!_hyperDriving) return;

        if (_centering)
        {
            if (Time.time >= _centerStartTime + _centeringTime)
            {
                _centering = false;
                _startHyperDrive.Invoke();
            }

            float fraction = ((Time.time - _centerStartTime)  / _centeringTime);
            fraction = Mathf.Min(1f, fraction);
            transform.position = (_middle + _startOffset) - (_startOffset * fraction);
        }
        else
        {
<<<<<<< Updated upstream
            transform.position += (Vector3.right * _hyperDrivespeed * Time.deltaTime);
            transform.localScale = new Vector3(_stretch, 1f, 1f);
=======

            transform.Translate(Vector3.right * _hyperDrivespeed * Time.deltaTime);
            transform.localScale = new Vector3(2f, 1f, 1f);
>>>>>>> Stashed changes
        }
        transform.rotation = Quaternion.identity;
    }
}
