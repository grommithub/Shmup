using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisjointedHurtBox : MonoBehaviour
{
    private EntityBase _base;
    internal bool isEnemy;
    private void Start()
    {
        _base = GetComponentInParent<EntityBase>();
        isEnemy = (_base is EnemyBase);
    }

    public void TakeDamage(int incomingDamage)
    {
        if(_base != null)
            _base.TakeDamage(incomingDamage);
    }
}
