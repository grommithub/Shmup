using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : EntityBase
{
    Vector2 _inputVector = new Vector2();
    [SerializeField] internal Weapon _weapon;

    private float _lastShot = 0f;

    protected override void Start()
    {
        base.Start();
    }
    
    void FixedUpdate()
    {
        Move();
        if (Input.GetButton(PlayerInput.shootButton) && Time.time > _lastShot + _weapon._shootSpeed)
            Shoot();
    }

    private void Shoot()
    {
        _weapon.Shoot(transform.position, transform.rotation, false, true);
        _lastShot = Time.time;
    }

    protected override void Move()
    {
        _inputVector.x = Input.GetAxis(PlayerInput.horizontalAxis);
        _inputVector.y = Input.GetAxis(PlayerInput.verticalAxis);

        _movementVector = _inputVector * _speed * Time.fixedDeltaTime;
        base.Move();
        RotateOnMove();
    }
}
