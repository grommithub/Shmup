using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    [Header("Burst Fire")]

    [SerializeField] private int _bursts, _shotsPerBurst;
    private float _maxY, _minY, _midY, _targetY;

    [SerializeField] private float _shootSpeed;
    private BossWeapon _bossWeapon;

    [Header("Big Shot")]
    [SerializeField] private float _pauseTime;

    protected override void Start()
    {
        _bossWeapon = GetComponent<BossWeapon>();

        base.Start();

        Camera cam = Camera.main;
        _minY = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0f)).y;
        _maxY = cam.ScreenToWorldPoint(Vector2.zero).y;
        _midY = (_minY + _maxY) / 2;

        StartCoroutine(ShootingCycle());
    }

    // Update is called once per frame
    private void Update()
    {
        if (_rb.velocity.magnitude < 1f) _rb.velocity = Vector2.zero;
        base.RotateOnMove();
    }

    private IEnumerator ShootingCycle()
    {
        for(int i = 0; i < _bursts; i++)
        {
            //makes the boss go between the top and bottom of the screen to reduce complete randomness
            if(i % 2 == 0)
                _targetY = UnityEngine.Random.Range(_minY, _midY);
            else
                _targetY = UnityEngine.Random.Range(_midY, _maxY);

            Vector3 nextPos = transform.position;
            nextPos.y = _targetY;
            float minMagnitude = (transform.position - nextPos).magnitude / 30f;

            while((transform.position - nextPos).magnitude >= minMagnitude)
            {
                Vector3 newPos = Vector3.Lerp(transform.position, nextPos, _speed * Time.deltaTime);
                _rb.velocity = (newPos - transform.position) / Time.deltaTime;
        yield return null;
            }

            transform.position = nextPos;

            for(int j = 0; j <_shotsPerBurst; j++)
            {
                _weapon.Shoot(transform.position, transform.rotation, true, false);
                print("Shoot");
                yield return new WaitForSeconds(_shootSpeed);
            }

        }
        Vector3 midPos = transform.position;
        midPos.y = _midY;
        float smallestDist = (transform.position - midPos).magnitude / 30f;

        while ((transform.position - midPos).magnitude >= smallestDist)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, midPos, _speed * Time.deltaTime);
            _rb.velocity = (newPos - transform.position) / Time.deltaTime;
            yield return null;
        }

        _bossWeapon.BigShot();

        yield return new WaitForSeconds(_pauseTime);
        StartCoroutine(ShootingCycle());
        yield return null;
    }


}
