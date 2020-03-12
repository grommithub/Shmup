using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    public GameObject shield_;
    public GameObject havoc_;
    public GameObject laser_;

    [HideInInspector] public Ability ability_;

    public void DevilTrigger()
    {
        if (ability_.name_ == "Havoc")
            havoc_.SetActive(true);
        else if (ability_.name_ == "Shield")
            shield_.SetActive(true);
        else if (ability_.name_ == "Laser")
            laser_.SetActive(true);
        else Debug.Log("GetAbility.cs: Error");
    }
}
