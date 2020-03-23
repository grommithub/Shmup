using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Don't bother trying to make sense of any of this

*/

public class SecondBoss : EnemyBase
{
    [SerializeField] private Transform _gunMuzzle;
    private Transform _gun;
    private SecondBossWeapon _secondWeapon;
    private Transform _player;

    private Vector2 screenBounds;
    private Vector2 middle;

    [Header("Prrrap")]
    [SerializeField] private int _shotsPerPrrap, _prrraps;
    [SerializeField] private float _prrrapInterval, _shotInterval, _gunRotationOnShoot, _aimSpeed;


    [SerializeField] private Transform[] _parts = new Transform[1];

    [Header("Roll")]
    [SerializeField] private float _rollspeed;
    [SerializeField] private float rollTime, _rollWaitTime;
    [SerializeField] private Sprite _spinningShell;

    [Header("SuperSpray")]
    [SerializeField] private int _superSprays;
    [SerializeField] private float _sprayDelay, sinMoveStartTime;
    
    private bool sineMovement;
    private float _sinMoveStartTime;
    [Header("Rockets")]
    [SerializeField] private int _rockets;
    [SerializeField] private float _rocketWaitTime;

    protected override void Start()
    {


        Camera cam = Camera.main;

        middle = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _secondWeapon = _gunMuzzle.GetComponent<SecondBossWeapon>();
        _gun = _gunMuzzle.parent;
        base.Start();

        StartCoroutine(MoveToMiddle());
    }

    private void Update()
    {
        if (sineMovement)
        {
            _movementVector = new Vector2(0f, Mathf.Cos((Time.time - _sinMoveStartTime) * 5) / 3);
            base.Move();
        }
    }

    private IEnumerator MoveToMiddle()
    {
        SoundPlayer.PlayBoss2Music();

        yield return new WaitForSeconds(2f);

        Vector2 destination = new Vector2(5.5f, 0f);
        while ((transform.position - (Vector3)destination).magnitude > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, destination, 6f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 6f * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(PrrrapPrrrap());
    }

    private IEnumerator PrrrapPrrrap()
    {



        for(int j = 0; j < _prrraps; j++)
        {
            for(int i = 0; i < _shotsPerPrrap; i++)
            {
                float nextShot = Time.time + _shotInterval;

                _gun.GetComponent<WiggleParts>().enabled = false;

                _secondWeapon.Shoot(_gunMuzzle.position, _gunMuzzle.rotation, true, false);
            
                _gun.localRotation = Quaternion.Euler(0f, 0f, _gun.localEulerAngles.z - _gunRotationOnShoot);

                while(Time.time < nextShot)
                {
                    Vector2 gunl = -_gun.transform.right;
                    if(_player != null)gunl = Vector2.Lerp(gunl, _player.position - ( _gunMuzzle.position), _aimSpeed * Time.deltaTime);
                    _gun.right = -gunl;

                    yield return null;
                }
                
            }
            yield return new WaitForSeconds(_prrrapInterval);
        }

        _gun.GetComponent<WiggleParts>().enabled = true;
        StartCoroutine(Roll());
    }

    private IEnumerator Roll()
    {
        while (_parts[0].localScale.x > 0.25f)
        {
            for(int i = 0; i < _parts.Length; i++)
            {
                _parts[i].localScale -= new Vector3(Time.deltaTime, 0f, 0f);
                yield return null;

            }
        }

        for(int i = 0; i < _parts.Length; i++)
        {
            _parts[i].gameObject.SetActive(false);
        }

        float rollSpeed = 0;
        
        while(rollSpeed < _rollspeed - 0.1f)
        {
            rollSpeed = Mathf.Lerp(rollSpeed, _rollspeed, 6f * Time.deltaTime);
            transform.Rotate(Vector3.forward, rollSpeed);
            yield return null;
        }

        SpriteRenderer shell = transform.Find("Shell").GetComponent<SpriteRenderer>();

        Sprite shellNoSpinSprite = shell.sprite;
        shell.sprite = _spinningShell;

        bool goingUp = false;
        bool goingRight = true;
        Vector2 direction = new Vector2();

        float stopTime = Time.time + rollTime;
        while (Time.time < stopTime)
        {
            if (goingRight)
            {
                direction.x = 1f;
                if (transform.position.x > 9f)
                {
                    goingRight = false;
                    yield return null;
                }
            }
            else
            {
                direction.x = -1;
                if (transform.position.x < -9)
                {
                    goingRight = true;
                    yield return null;
                }
            }
            if (goingUp)
            {
                direction.y = 1f;
                if (transform.position.y > 5f)
                {
                    goingUp = false;
                    yield return null;
                }
            }
            else
            {
                direction.y = -1;
                if (transform.position.y < -5)
                {
                    goingUp = true;
                    yield return null;
                }
            }

            _rb.velocity = direction * _speed;
            transform.Rotate(Vector3.forward, rollSpeed);
            yield return null;

        }
        _rb.velocity = Vector2.zero;
        while(rollSpeed > 0.1f)
        {
            transform.Rotate(Vector3.forward, rollSpeed);
            rollSpeed = Mathf.Lerp(rollSpeed, 0f, 6f * Time.deltaTime);
            yield return null;
        }
        shell.sprite = shellNoSpinSprite;

        Vector2 destination = new Vector2(5.5f, 0f);
        while((transform.position - (Vector3)destination).magnitude > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, destination, 6f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 6f * Time.deltaTime);
            yield return null;
        }
        while(_parts[0].localScale.x < 0.95f)
        {
            for(int i = 0; i < _parts.Length; i++)
            {
                _parts[i].gameObject.SetActive(true);
                _parts[i].localScale += new Vector3(Time.deltaTime, 0f, 0f);
            }
            yield return null;
        }
        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].localScale = new Vector3(1f,1f,1f);
        }
        yield return new WaitForSeconds(_rollWaitTime);
        StartCoroutine(SuperSpray());
    }

    private IEnumerator SuperSpray()
    {
        sinMoveStartTime = Time.time;
        sineMovement = true;
        for(int i = 0; i < _superSprays; i++)
        {
            _secondWeapon.SuperSpray(i % 2 == 0);   
            yield return new WaitForSeconds(_sprayDelay);
        }
        yield return new WaitForSeconds(1f);
        sineMovement = false;
        _rb.velocity = Vector2.zero;
        _secondWeapon.ShootHarpoon();
        StartCoroutine(Rockets());
    }

    private IEnumerator Rockets()
    {
        for(int i = 0; i < _rockets; i++)
        {
            _secondWeapon.ShootRocket();
            yield return new WaitForSeconds(_rocketWaitTime);
        }
        StartCoroutine(PrrrapPrrrap());
    }

    protected override void Die()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * 5f, ForceMode2D.Impulse);
            transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-10f, 10f), ForceMode2D.Impulse);
        }
        transform.DetachChildren();

        base.Die();
    }
}
