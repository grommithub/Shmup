using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossWeapon : Weapon
{
    [SerializeField] private GameObject harpoon, rocket;
    [SerializeField] private float _spreadDegrees;
    public void ShootHarpoon()
    {
        Shoot(transform.position, transform.rotation, true, false, harpoon);
    }
    public void ShootRocket()
    {
        Shoot(transform.position, transform.rotation, true, true, rocket);
    }
    public void SuperSpray(bool offset)
    {
        for (int i = 0; i <= 4; i++)
        {
            float degrees = 0f;
            if (offset) degrees = _spreadDegrees / 2;

            Quaternion rotation = Quaternion.Euler(0, 0, i * _spreadDegrees + degrees);
            Quaternion backRotation = Quaternion.Euler(0, 0, -i * _spreadDegrees + degrees);
            base.Shoot(transform.position, transform.rotation * rotation, true, false);
            base.Shoot(transform.position, transform.rotation * backRotation, true, false);
        }
    }
}
