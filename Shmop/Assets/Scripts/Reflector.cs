using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var proj = other.GetComponent<ProjectileBase>();
        if (proj != null)
            proj.GetDeflected();
    }
}
