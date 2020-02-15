using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : EntityBase
{
    Vector2 _inputVector = new Vector2();
    [SerializeField] internal Weapon _weapon;

    [SerializeField] private Transform _bulletStart;
    private float _lastShot = 0f;

    protected override void Start()
    {
        base.Start();
    }
    
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (_weapon == null) return;
        if (Input.GetButton(PlayerInput.shootButton) && Time.time > _lastShot + _weapon._shootSpeed)
        {
            _weapon.Shoot(_bulletStart.position, transform.rotation, false, true);
            _lastShot = Time.time;

        }
    }
    public void GiveHealth(int healthBonus)
    {
        _health += healthBonus;
        _health = Mathf.Min(_health, _maxHealth);

        _colourController.SetTemporaryColor(Color.green, 0.1f * healthBonus);
    }

    public override void TakeDamage(int incomingDamage)
    {
        base.TakeDamage(incomingDamage);
        _colourController.SetTemporaryColor(Color.red, 0.1f);
    }

    protected override void Move()
    {
        _inputVector.x = Input.GetAxis(PlayerInput.horizontalAxis);
        _inputVector.y = Input.GetAxis(PlayerInput.verticalAxis);

        _movementVector = _inputVector * _speed;
        base.Move();
        RotateOnMove();
    }
}
