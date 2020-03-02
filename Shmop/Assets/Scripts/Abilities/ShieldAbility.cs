//responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Shield")] //creating an option to create such asset in unity menu
public class ShieldAbility : Ability
{
    public float shieldDuration_; 

    private ShieldTrigger shield_; //a variable that is going to store a trigger script as a component

    public override void Initialize(GameObject obj_in)
    {
        shield_ = obj_in.GetComponent<ShieldTrigger>(); //getting reference to trigger script
        shield_.shieldDuration_ = this.shieldDuration_; //assigning 
    }

    public override void TriggerAbility()
    {
        shield_.DevilTrigger(); //calling trigger function from trigger script
    }
}
