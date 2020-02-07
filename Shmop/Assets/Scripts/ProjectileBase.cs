using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ProjectileBase : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] internal int damage;
    internal bool isEnemy;
    internal bool goingRight;
    private float directionMultiplier = 1;
    protected Rigidbody2D _rb;

    [SerializeField] private GameObject explosion;
    private void Start()
    {
        if (!goingRight) directionMultiplier *= -1;
        _rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        _rb.velocity = transform.up * speed * directionMultiplier;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        print("hit");
        EntityBase ent = collider.transform.GetComponent<EntityBase>();
        //if(typeof(ent).IsAssignableFrom(typeof(PlayerBehaviour)))
        if((ent is PlayerBehaviour && isEnemy) || (ent is EnemyBase && !isEnemy))
        {
            ent.TakeDamage(damage);
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);

            float r = Random.Range(.75f, 1.25f);

            expl.transform.localScale = new Vector3(r, r, 1.0f);
            
            Destroy(expl, 1f);

            Destroy(this.gameObject, 0f);
        }
    }
}
