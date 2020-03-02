using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : ProjectileBase
{
    private Transform _player;
    [SerializeField] private float _rotationSpeed, maxExplosionDistance, _lifeSpan;
    [SerializeField] private int explosions;



    private float _deathTime;
    protected override void Start()
    {


        _deathTime = Time.time + _lifeSpan;
        goingRight = true;
        isEnemy = true;
        base.Start();
        var p = FindObjectOfType<PlayerBehaviour>();
        if(p != null)
            _player = p.transform;
    }

    protected override void FixedUpdate()
    {
        if (Time.time > _deathTime) Explode();

        if(_player != null)
            transform.up = Vector3.Slerp(transform.up, (_player.position - transform.position).normalized, _rotationSpeed * Time.deltaTime);
        _rb.velocity = transform.up * speed;
    }

    protected override void Explode()
    {
        for (int i = 0; i < explosions; i++)
        {
            float x, y, s;
            x = Random.Range(-maxExplosionDistance, maxExplosionDistance);
            y = Random.Range(-maxExplosionDistance, maxExplosionDistance);
            s = Random.Range(0.75f, 1.25f);

            Vector3 offset = new Vector3(x, y, 0f);

            GameObject expl = Instantiate(explosion, transform.position + offset, Quaternion.identity);
            expl.transform.localScale = new Vector3(s, s, 1f);
            expl.GetComponent<AnimationDelay>().SetWaitTime(0.15f * i);
        }
        Destroy(gameObject);
    }
}
