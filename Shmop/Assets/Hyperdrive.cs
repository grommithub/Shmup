using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperdrive : MonoBehaviour
{
    private Vector3 _middle = new Vector3();
    private bool _hyperDriving;

    private bool _centering;

    [SerializeField] private float _hyperDrivespeed = 30f;


    [SerializeField]private float goToMiddleSpeed = 3f;
    private void Start()
    {
        _middle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/3, Screen.height/2, 0f));
        _middle.z = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            _hyperDriving = true;
            _centering = true;
        }
        if (!_hyperDriving) return;
        if (_centering)
            transform.position = Vector3.Lerp(transform.position, _middle, goToMiddleSpeed * Time.deltaTime);
        else
            transform.position += (Vector3.right * _hyperDrivespeed * Time.deltaTime);

        if ((transform.position - _middle).magnitude < 0.05f)
        {
            _centering = false;
        }
    }


}
