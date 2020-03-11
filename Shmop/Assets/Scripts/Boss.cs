using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    private Collider2D _collider;

    [Header("Burst Fire")]

    [SerializeField] private int _bursts;
    [SerializeField] private int _shotsPerBurst;
    private float _maxY, _minY, _midY, _targetY;

    [SerializeField] private float _shootSpeed;
    private BossWeapon _bossWeapon;

    [Header("Entry")]
    [SerializeField] private float _entrySpeed;
    [SerializeField] private float _entryWait;

    [Header("Big Shot")]
    [SerializeField] private float _pauseTime;

    [Header("Spray")]
    [SerializeField] private float _sprayTime;
    [SerializeField] private float _sprayInterval;

    [Header("Rockets")]
    [SerializeField] private int _rocketsPerSpray;
    [SerializeField] private int _sprays;
    [SerializeField] private float _angleDifferencePerRocket, _waitPerRocket, _waitPerSpray;
    private bool sineMovement;
    private float _sinMoveStartTime;

    protected override void Start()
    {

        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        _bossWeapon = GetComponent<BossWeapon>();

        base.Start();

        Camera cam = Camera.main;
        _maxY = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 0.9f, 0f)).y;
        _minY = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 0.1f, 0)).y;
        _midY = (_minY + _maxY) / 2;

        StartCoroutine(MoveToMiddle());
    }

    // Update is called once per frame
    private void Update()
    {
        if (_rb.velocity.magnitude < 1f) _rb.velocity = Vector2.zero;
        base.RotateOnMove();

        if(sineMovement)
        {
            _movementVector = new Vector2(0f, Mathf.Cos((Time.time - _sinMoveStartTime) * 2));
            base.Move();
        }
    }

    private IEnumerator MoveToMiddle()
    {
        SoundPlayer.PlayBossIntroMusic();

        Vector3 midPos = new Vector3(5f, _midY);
        float smallestDist = (transform.position - midPos).magnitude / 30f;

        while ((transform.position - midPos).magnitude >= smallestDist)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, midPos, _entrySpeed * Time.deltaTime);
            _rb.velocity = (newPos - transform.position) / Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_entryWait);
        _collider.enabled = true;
        SoundPlayer.PlayBossMusic();
        StartCoroutine(ShootingCycle());
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
        StartCoroutine(BulletHell());
        yield return null;
    }
    private IEnumerator BulletHell()
    {

        bool goingUp = false;
        Vector2 direction = new Vector2();
        float sprayStopTime = Time.time + _sprayTime;
        float shootTime = Time.time + _sprayInterval;

        while (Time.time < sprayStopTime)
        {
            if (goingUp)
            {
                direction = Vector2.up;
                if (transform.position.y > _maxY)
                {
                    goingUp = false;
                    yield return null;
                }
            }
            else
            {
                direction = Vector2.down;
                if (transform.position.y < _minY)
                {
                    goingUp = true;
                    yield return null;
                }
            }
            _rb.velocity = direction * _speed;

            if(Time.time > shootTime)
            {
                _bossWeapon.Spray();
                shootTime = Time.time + _sprayInterval;
            }

            yield return null;

        }
    
        _rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(_pauseTime);

        StartCoroutine(Rockets());
    }

    private IEnumerator Rockets()
    {
        Vector3 midPos = transform.position;
        midPos.y = _midY;
        float smallestDist = (transform.position - midPos).magnitude / 30f;

        while ((transform.position - midPos).magnitude >= smallestDist)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, midPos, _speed * Time.deltaTime);
            _rb.velocity = (newPos - transform.position) / Time.deltaTime;
            yield return null;
        }

        _sinMoveStartTime = Time.time;
        sineMovement = true;

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i <= 4; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, i * 20f);
                Quaternion backRotation = Quaternion.Euler(0, 0, -i * 20f);
                _bossWeapon.ShootRocket(rotation);
                _bossWeapon.ShootRocket(backRotation);
                yield return new WaitForSeconds(_waitPerRocket);
            }
            yield return new WaitForSeconds(_waitPerSpray);
        }
        yield return null;
        sineMovement = false;

        yield return new WaitForSeconds(_pauseTime);
        StartCoroutine(ShootingCycle());
    }
}
