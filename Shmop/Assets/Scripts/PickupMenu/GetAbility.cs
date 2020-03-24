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
        {
            havoc_.SetActive(true);
            havoc_.GetComponent<AbilityCooldown>().ability_ = this.ability_;
            havoc_.GetComponent<AbilityCooldown>().Reinitialize();
        }   
        else if (ability_.name_ == "Shield")
        {
            shield_.SetActive(true);
            shield_.GetComponent<AbilityCooldown>().ability_ = this.ability_;
            havoc_.GetComponent<AbilityCooldown>().Reinitialize();
        } 
        else if (ability_.name_ == "Laser")
        {
            laser_.SetActive(true);
            laser_.GetComponent<AbilityCooldown>().ability_ = this.ability_;
            havoc_.GetComponent<AbilityCooldown>().Reinitialize();
        }
        else Debug.Log("GetAbility.cs: Error");
    }
}
