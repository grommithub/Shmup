using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : Weapon
{
    [Header("Big shot")]
    [SerializeField] private GameObject _bigShot;
    [SerializeField] private float _spreadDegrees, _spreadBullets;
    public void BigShot()
    {
        print("Big shot");

        for (int i = 1; i <= 4; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, i * _spreadDegrees);
            Quaternion backRotation = Quaternion.Euler(0, 0, -i * _spreadDegrees);
            base.Shoot(transform.position, transform.rotation * rotation, true, false);
            base.Shoot(transform.position, transform.rotation * backRotation, true, false);

        }

        base.Shoot(transform.position, transform.rotation, true, false, _bigShot);

    }
}
