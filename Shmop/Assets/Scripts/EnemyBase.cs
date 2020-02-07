using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    [SerializeField] protected int _onCollideDamage = 1;
    [SerializeField] protected Weapon _weapon;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.transform.GetComponent<PlayerBehaviour>();
        if(player != null)
        {
            player.TakeDamage(_onCollideDamage);
        }
    }

    public override void TakeDamage(int incomingDamage)
    {
        base.TakeDamage(incomingDamage);
        if(_colourSprite != null)
        {
            _colourSprite.ChangeColour(Color.red, 0.1f);
        }
    }

    void Shoot()
    {
        if (_weapon != null) _weapon.Shoot(transform.position, transform.rotation, true, true);
    }
}
