using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    [SerializeField] private float verticality, turningSpeed;

    [SerializeField] private float shootInterval;
    private float lastshot;

    protected override void Move()
    {
        _movementVector.y = Mathf.Cos(Time.time * turningSpeed) * verticality;
        _movementVector.x = -_speed;
        Vector2.ClampMagnitude(_movementVector, _speed);
        base.Move();
    }
    private void FixedUpdate()
    {
        Move();
        RotateOnMove();

        if(Time.time > lastshot + shootInterval && _weapon != null)
        {
            _weapon.Shoot(transform.position, transform.rotation, true, false);
            lastshot = Time.time;
        }
    }
}
