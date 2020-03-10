//responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Laser")] //creating an option to create such asset in unity menu
public class RaycastAbility : Ability
{
    //public float width_; 
    //public float damage_;
    public float laserDuration_;
    public int laserDamage_;
    
    private RaycastTrigger raycast_;

    public override void Initialize(GameObject obj_)
    {
        raycast_ = obj_.GetComponent<RaycastTrigger>();

        raycast_.laserDuration_ = this.laserDuration_;
        raycast_.laserDamage_ = this.laserDamage_;
    }

    public override void TriggerAbility()
    {
        raycast_.DevilTrigger();
    }
}
