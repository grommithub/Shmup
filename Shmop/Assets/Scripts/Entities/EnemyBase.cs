using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    [SerializeField] protected int _onCollideDamage = 1;
    [SerializeField] protected Weapon _weapon;
    [SerializeField] protected DropItem _drop;

    protected override void Start()
    {
        _drop = GetComponent<DropItem>();
        _weapon = GetComponent<Weapon>();
        base.Start();
    }
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
        SoundPlayer.PlayOneShot(SoundPlayer._soundPlayer.enemyHurt);
        base.TakeDamage(incomingDamage);
        if(_colourSprite != null)
        {
            _colourSprite.ChangeColour(Color.red, 0.1f);
        }
    }

    protected override void Die()
    {
        if (_drop != null)
        {
            _drop.GetRandomDrop();
        }
        else print("thing doesn't exist");

        base.Die();
    }
    private void Shoot()
    {
        if (_weapon != null) _weapon.Shoot(transform.position, transform.rotation, true, true);
    }
}
