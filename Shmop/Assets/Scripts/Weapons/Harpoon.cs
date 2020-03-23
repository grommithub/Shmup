using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : ProjectileBase
{
    private Vector2 _startPosition, _endPosition;
    private LineRenderer _lineRenderer;
    [SerializeField] private float _pullBackSpeed = 1f, _maxLength;
    private float _pullBackStartTime;

    private EntityBase _target;

    private bool _retracting;
    protected override void Start()
    {
        _startPosition = transform.position;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, _startPosition);
        base.Start();
        damage = 0;
    }
    protected override void FixedUpdate()
    {
        _lineRenderer.SetPosition(1, transform.position);
        print((_lineRenderer.GetPosition(0) - _lineRenderer.GetPosition(1)).magnitude);
        if ((_lineRenderer.GetPosition(0) - _lineRenderer.GetPosition(1)).magnitude > _maxLength)
        {
            _retracting = true;
            GetComponent<Collider2D>().enabled = false;
        }
        Move();
    }

    protected override void Move()
    {
        if(!_retracting)
        {
            base.Move();
        }
        else
        {
            Vector3 velocity = ((Vector3)_startPosition - transform.position);
            velocity = Vector3.ClampMagnitude(velocity, _pullBackSpeed);
            _rb.velocity = velocity;
            if (velocity.magnitude < 0.1f)
            {
                if(_target != null)_target.TakeDamage(10000000);
                Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {

        EntityBase ent = collider.transform.GetComponent<EntityBase>();
        if (ent != null)
        {
            if ((ent is PlayerBehaviour && isEnemy) || (ent is EnemyBase && !isEnemy))
            {
                ent.enabled = false;
                _retracting = true;
                _pullBackStartTime = Time.time;
                _endPosition = transform.position;
                ent.transform.SetParent(gameObject.transform);
                
                ent.GetComponent<Rigidbody2D>().simulated = false;

                _target = ent;
            }
        }
        //var hurtbox = collider.GetComponent<DisjointedHurtBox>();
        //if (hurtbox != null)
        //{
        //    if (hurtbox.isEnemy != isEnemy)
        //    {
        //        hurtbox.TakeDamage(damage);
        //        Explode();
        //        Destroy(this.gameObject, 0f);
        //    }
        //}

    }
}
