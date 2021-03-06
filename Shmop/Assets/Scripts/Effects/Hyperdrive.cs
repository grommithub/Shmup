﻿using System.Collections;
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
    [SerializeField] private GameObject _textBox;

    [SerializeField] private float _centeringTime = 5f;
    [SerializeField] private float _hyperDrivespeed = 30f;

    [SerializeField] private float _stretch;
    
    private bool _hyperDriving;
    private bool _centering;
    private bool _centersShip;

    private Vector2 _middle = new Vector2();
    private Vector2 _startOffset = new Vector2();

    private TextBox _textBoxComponent;
    private Rigidbody2D _rb;

    private float _centerStartTime;

    private void Start()
    {
        _middle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 3, Screen.height / 2, 0f));
        _textBoxComponent = _textBox.GetComponent<TextBox>();
        _rb = GetComponent<Rigidbody2D>();
        _centersShip = false;
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
        
        _textBoxComponent.inHyperDrive_ = true;

        _startMiniHyperDrive.Invoke();
        _rb.velocity = Vector2.zero;
        while (_textBoxComponent.inHyperDrive_ == true)
        {
            yield return null;
        }
        //yield return new WaitUntil(() => GetComponent<TextBox>().inHyperDrive_ == false);
        _stopMiniHyperDrive.Invoke();
        yield return null;
    }

    public void DoCentering()
    {
        StartCoroutine(CenterShip());
    }

    IEnumerator CenterShip()
    {
        _centerStartTime = Time.time;
        _centersShip = true;
        _startOffset = (Vector2)transform.position - _middle;

        while (_centersShip)
        {
            if (Time.time >= _centerStartTime + _centeringTime)
            {
                _centersShip = false;
            }

            float fraction = ((Time.time - _centerStartTime) / _centeringTime);
            fraction = Mathf.Min(1f, fraction);
            transform.position = (_middle + _startOffset) - (_startOffset * fraction);

            yield return null;
        }
    }
}
