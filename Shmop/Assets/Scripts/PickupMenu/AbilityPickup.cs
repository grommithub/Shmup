using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/AbilityPickup")]
public class AbilityPickup : Upgrades
{
    public Ability ability_;

    private GetAbility getAbility_;

    public override void Initialize(GameObject obj_in)
    {
        getAbility_ = obj_in.GetComponent<GetAbility>();

        getAbility_.ability_ = this.ability_; 
    }

    public override void UseUpgrade()
    {
        getAbility_.DevilTrigger();
    }
}
