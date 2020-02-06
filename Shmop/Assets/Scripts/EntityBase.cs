using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ColorSprite))]

public class EntityBase : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    private ColorSprite _colorSprite;
    protected Rigidbody2D _rb;

    [SerializeField] private float shipRotationSpeed, shipRotationAmount;
    protected Vector2 _movementVector;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _colorSprite = GetComponent<ColorSprite>();
    }

    public int Health
    {
        get
        {
            return _health;
        }
    }

    protected virtual void Move()
    {
        _rb.velocity = _movementVector * _speed;
    }

    public void TakeDamage(int incomingDamage)
    {
        _health -= incomingDamage;
        if(_health <= 0)
        {
            //_colorSprite.
            Die();
        }
    }

    protected void RotateOnMove()
    {
        float rotation = 0;
        if (Mathf.Abs(_rb.velocity.y) > 0.1f)
            rotation = Mathf.Sign(_rb.velocity.y) * shipRotationAmount;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotation), shipRotationSpeed * Time.deltaTime);

    }

    private void Die()
    {
        print("DIE!");
        Destroy(this.gameObject, 0.0f);
    }
}
