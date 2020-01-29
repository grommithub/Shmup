using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    [SerializeField] protected int _onCollideDamage = 1;
    [SerializeField] protected Weapon _weapon;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var player = collision.transform.GetComponent<PlayerBehaviour>();
        if(player != null)
        {
            player.TakeDamage(_onCollideDamage);
        }
    }

    void Shoot()
    {
        if (_weapon != null) _weapon.Shoot(transform.position, transform.rotation, true, true);
    }
}
