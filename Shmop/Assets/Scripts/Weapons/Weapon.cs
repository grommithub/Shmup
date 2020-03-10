using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float _shootSpeed;
    public GameObject projectile;

    internal bool canShoot = true;

    public virtual void Shoot(Vector2 position, Quaternion rotation, bool enemy, bool goingRight)
    {
        if (!canShoot) return;
        GameObject proj = Instantiate(projectile, position, Quaternion.identity);
        proj.transform.up = Quaternion.AngleAxis(-90f + rotation.eulerAngles.z, Vector3.forward) * Vector3.up;

        ProjectileBase projectileBase = proj.GetComponent<ProjectileBase>();
        if(projectileBase != null)
        {
            projectileBase.isEnemy = enemy;
            projectileBase.goingRight = goingRight;
            proj.GetComponent<SpriteRenderer>().flipY = !goingRight;
        }
    }
    public virtual void Shoot(Vector2 position, Quaternion rotation, bool enemy, bool goingRight, GameObject p)
    {
        GameObject proj = Instantiate(p, position, Quaternion.identity);
        proj.transform.up = Quaternion.AngleAxis(-90f + rotation.eulerAngles.z, Vector3.forward) * Vector3.up;

        ProjectileBase projectileBase = proj.GetComponent<ProjectileBase>();
        if (projectileBase != null)
        {
            projectileBase.isEnemy = enemy;
            projectileBase.goingRight = goingRight;
            proj.GetComponent<SpriteRenderer>().flipY = !goingRight;
        }
    }
}
