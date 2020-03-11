using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrigger : MonoBehaviour
{

    private SpriteRenderer _ballRenderer;
    private Weapon _playerWeapon;
    private bool firing;
    private float _startTime;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] internal float _damageInterval, _duration, _animationSpeed, _animationDrift;
    [SerializeField] internal EnemyBase _enemyHit;
    [SerializeField] private float maxExplosionDist;
    [SerializeField] private GameObject _explosion;

    private float lastHit;
    private void Start()
    {
        _playerWeapon = GetComponentInParent<Weapon>();
        if(_playerWeapon == null)
        {
            _playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _ballRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
    }
    public void DevilTrigger()
    {

        _spriteRenderer.enabled = true;
        _ballRenderer.enabled = true;
        firing = true;
        _startTime = Time.time;
    }

    private void Update()
    {
        if(firing)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            if (hit)
            {
                var enemy = hit.transform.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    if(enemy != _enemyHit)
                    {
                        SetLength(hit.point);
                        _enemyHit = enemy;
                        SpawnExplosion();
                        enemy.TakeDamage(1);
                        lastHit = Time.time;
                    }
                    else if (Time.time > lastHit + _damageInterval)
                    {
                        SetLength(hit.point);
                        _enemyHit = enemy;
                        lastHit = Time.time;
                        SpawnExplosion();
                        _enemyHit.TakeDamage(1);
                    }
                }

            }
            else
            {
                _enemyHit = null;
                Vector2 scale = new Vector2(20f, 1f);
                _spriteRenderer.size = scale;
            }
            _spriteRenderer.material.mainTextureOffset = new Vector2(Mathf.Sin(Time.time * _animationSpeed) * _animationDrift, 0);//(Time.time % (1.69f - 0.94f) + 0.94f, 0f);
            _spriteRenderer.size = new Vector2(_spriteRenderer.size.x,  1f + (Mathf.Sin(Time.time * _animationSpeed) * _animationDrift * 10f));
            if(Time.time > _startTime + _duration)
            {
                firing = false;

                _spriteRenderer.enabled = false;
                _ballRenderer.enabled = false;
            }
        }
        _playerWeapon.canShoot = !firing;
    }
    private void SetLength(Vector3 point)
    {
        float distance = (point - transform.position).magnitude;
        Vector2 scale = new Vector2(distance, 1f);
        _spriteRenderer.size = scale;
    }
    private void SpawnExplosion()
    {
        float[] coords = new float[2];
        coords[0] = Random.Range(-maxExplosionDist, maxExplosionDist);
        coords[1] = Random.Range(-maxExplosionDist, maxExplosionDist);
        GameObject e = Instantiate(_explosion, transform.position + transform.right * _spriteRenderer.size.x + new Vector3(coords[0], coords[1],0f), Quaternion.identity);

        e.GetComponent<AnimationDelay>().SetWaitTime(coords[0]);
    }
}



//public class RaycastTrigger : MonoBehaviour
//{
//    [SerializeField] private GameObject laser_;
//    [SerializeField] private GameObject parent_;
//    private GameObject beam_;
//    private GameObject target_;
//    private EnemyBase enemyComponent_;

//    private Weapon playerWeapon_;

//    [HideInInspector] public LineRenderer lr_;
//    [HideInInspector] public float laserDuration_;
//    [HideInInspector] public int laserDamage_;

//    // Start is called before the first frame update
//    private void Start()
//    {
//        playerWeapon_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
//        //beam_ = Instantiate(laser_);
//        //beam_.transform.SetParent(parent_.transform);
//        lr_ = beam_.GetComponent<LineRenderer>();

//        //StartCoroutine(StartCountdown());
//    }

//    // Update is called once per frame
//    private void Update()
//    {
//        //beam_.transform.position = this.transform.position;
//        lr_.SetPosition(0, this.transform.position);
//        //lr_.SetPosition(1, this.transform.right * 500);
//        RaycastHit2D hit_;
//        hit_ = Physics2D.Raycast(transform.position, transform.right);
//        if (hit_.collider)
//        {
//            if(hit_.collider.CompareTag("Enemy"))
//            {
//                lr_.SetPosition(1, hit_.point);
//                target_ = hit_.transform.gameObject;
//                enemyComponent_ = target_.GetComponent<EnemyBase>();
//                enemyComponent_.TakeDamage(laserDamage_);
//            }
//        }
//        else
//        {
//            lr_.SetPosition(1, this.transform.right * 500);
//        }
//    }

//    public void DevilTrigger()
//    {
//        beam_ = Instantiate(laser_);
//        beam_.transform.SetParent(parent_.transform);
//        lr_ = beam_.GetComponent<LineRenderer>();

//        //PlayerInput.shootButton = "";
//        playerWeapon_.enabled = false;

//        StartCoroutine(StartCountdown());

//        enabled = true;

//    }

//    IEnumerator StartCountdown()
//    {
//        yield return new WaitForSeconds(laserDuration_);
//        Destroy(beam_);

//        playerWeapon_.canShoot = true;

//        //PlayerInput.shootButton = "Jump";

//        enabled = false;
//    }
//}
