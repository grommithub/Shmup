using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    [SerializeField] private float verticality, turningSpeed;

    [SerializeField] private float shootInterval;
    private float _lastshot;
    private float _spawnTime;

    protected override void Start()
    {
        //_spawnTime = Time.time;
        _lastshot = Time.time;
        base.Start();
    }
    protected override void Move()
    {
        _movementVector.y = Mathf.Cos((Time.time - _spawnTime) * turningSpeed) * verticality;
        _movementVector.x = -_speed;
        Vector2.ClampMagnitude(_movementVector, _speed);
        base.Move();
    }
    private void FixedUpdate()
    {
        Move();
        RotateOnMove();

        if(Time.time > _lastshot + shootInterval && _weapon != null)
        {
            _weapon.Shoot(transform.position - Vector3.right, transform.rotation, true, false);
            _lastshot = Time.time;
        }

        if (transform.position.x < -10f) Destroy(this.gameObject);
    }
}
