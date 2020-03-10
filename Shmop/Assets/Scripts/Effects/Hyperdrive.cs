using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hyperdrive : MonoBehaviour
{



    public UnityEvent _startMiniHyperDrive;
    public UnityEvent _stopMiniHyperDrive;

    [SerializeField] internal float _miniHyperDriveTime = 3f;

    [SerializeField] private UnityEvent _startHyperDrive;
    [SerializeField] private UnityEvent _startCentering;
    
    [SerializeField] private float _centeringTime = 5f;
    [SerializeField] private float _hyperDrivespeed = 30f;

    [SerializeField] private float _stretch;
    
    private bool _hyperDriving;
    private bool _centering;

    private Vector2 _middle = new Vector2();
    private Vector2 _startOffset = new Vector2();
    
    private float _centerStartTime;

    private void Start()
    {
        _middle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/3, Screen.height/2, 0f));
    }

    public void StartHyperDrive()
    {
        _startOffset = (Vector2)transform.position - _middle;
        _centerStartTime = Time.time;

        _startCentering.Invoke();
        _hyperDriving = true;
        _centering = true;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G)) StartCoroutine(MiniHyperDrive());

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

            transform.Translate(Vector3.right * _hyperDrivespeed * Time.deltaTime);
            transform.localScale = new Vector3(_stretch, 1f, 1f);
        }
        transform.rotation = Quaternion.identity;
    }

    public IEnumerator MiniHyperDrive()
    {
        yield return new WaitForSeconds(_miniHyperDriveTime / 2);
        _startMiniHyperDrive.Invoke();
        yield return new WaitForSeconds(_miniHyperDriveTime / 2);
        _stopMiniHyperDrive.Invoke();
        yield return null;
    }
}
