using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(ColorSprite))]

public class EntityBase : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    protected static ColorController _colourController;
    protected ColorSprite _colourSprite;
    protected Rigidbody2D _rb;

    [SerializeField] protected GameObject explosion;
    [SerializeField] private int explosions;
    [SerializeField] private float maxExplosionDistance;

    [SerializeField] private float _rotationSpeed, _rotationAmount;
    protected Vector2 _movementVector;

    protected virtual void Start()
    {
        _health = _maxHealth;

        _rb = GetComponent<Rigidbody2D>();
        _colourController = GameObject.FindObjectOfType<ColorController>();
        _colourSprite = GetComponent<ColorSprite>();

    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    protected virtual void Move()
    {
        _rb.velocity = _movementVector * _speed;
    }

    public virtual void TakeDamage(int incomingDamage)
    {
        _health -= incomingDamage;
        if (_health <= 0)
        {
            Die();
        }
    }

    protected void RotateOnMove()
    {
        float rotation = 0;
        if (Mathf.Abs(_rb.velocity.y) > 0.1f)
            rotation = Mathf.Sign(_rb.velocity.y) * _rotationAmount;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotation), _rotationSpeed * Time.deltaTime);

    }

    protected virtual void Die()
    {
        for(int i = 0; i < explosions; i++)
        {
            float x, y, s;
            x = Random.Range(-maxExplosionDistance, maxExplosionDistance);
            y = Random.Range(-maxExplosionDistance, maxExplosionDistance);
            s = Random.Range(0.75f, 1.25f);

            Vector3 offset = new Vector3(x, y, 0f);

            GameObject expl = Instantiate(explosion, transform.position + offset, Quaternion.identity);
            expl.transform.localScale = new Vector3(s, s, 1f);
            expl.GetComponent<AnimationDelay>().SetWaitTime(0.05f * i);
        }

        Destroy(this.gameObject, 0.0f);
    }
}
