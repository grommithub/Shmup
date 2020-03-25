using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] private Sprite _muzzleFlash, _normalSprite;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private AudioClip startSound;

    [SerializeField] protected float speed;
    [SerializeField] internal int damage;
    internal bool isEnemy;
    internal bool goingRight;
    protected float directionMultiplier = 1;
    protected Rigidbody2D _rb;

    bool _firstFrame = true;

    [SerializeField] protected GameObject explosion;
    protected virtual void Start()
    {
        if(startSound != null) SoundPlayer.PlayOneShot(startSound);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalSprite = _spriteRenderer.sprite;
        damage = 1;

        if (!goingRight) directionMultiplier *= -1;
        _rb = GetComponent<Rigidbody2D>();

    }
    protected virtual void FixedUpdate()
    {
        if (_firstFrame && _muzzleFlash != null)
        {
            _spriteRenderer.sprite = _muzzleFlash;
            _firstFrame = false;
        }
        else
            _spriteRenderer.sprite = _normalSprite;

        Move();
    }

    protected virtual void Move()
    {
        _rb.velocity = transform.up * speed * directionMultiplier;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {

        EntityBase ent = collider.transform.GetComponent<EntityBase>();
        if(ent != null)
        {
            if((ent is PlayerBehaviour && isEnemy) || (ent is EnemyBase && !isEnemy))
            {
                ent.TakeDamage(damage);

                Explode();

                Destroy(this.gameObject, 0f);
                return;
            }
        }
        var hurtbox = collider.GetComponent<DisjointedHurtBox>();
        if (hurtbox != null)
        {
            if (hurtbox.isEnemy != isEnemy)
            {
                hurtbox.TakeDamage(damage);
                Explode();
                Destroy(this.gameObject, 0f);
            }
        }

    }

    protected virtual void Explode()
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
        float r = Random.Range(.75f, 1.25f);

        expl.transform.localScale = new Vector3(r, r, 1.0f) * transform.localScale.x;
    }

    public void GetDeflected()
    {
        transform.up = -transform.up;
        isEnemy = !isEnemy;
    }
}
