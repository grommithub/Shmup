using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : Weapon
{
    [Header("Big shot")]
    [SerializeField] private GameObject _bigShot;
    [SerializeField] private GameObject _homingRocket;
    [SerializeField] private float _spreadDegrees, _spreadBullets;
    public void BigShot()
    {
        Spray();
        base.Shoot(transform.position, transform.rotation, true, false, _bigShot);
    }
    public void Spray()
    {
        for (int i = 0; i <= 4; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, i * _spreadDegrees);
            Quaternion backRotation = Quaternion.Euler(0, 0, -i * _spreadDegrees);
            base.Shoot(transform.position, transform.rotation * rotation, true, false);
            base.Shoot(transform.position, transform.rotation * backRotation, true, false);
        }
    }

    public void ShootRocket(Quaternion rotation)
    {
        base.Shoot(transform.position, rotation, true, true, _homingRocket);
    }
}
