using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/AbilityPickup")]
public class AbilityPickup : Upgrades
{
    public Ability ability_;
    public GameObject abilityHolder_;

    private GetAbility getAbility_;

    public override void Initialize(GameObject obj_in)
    {
        getAbility_ = obj_in.GetComponent<GetAbility>();

        getAbility_.ability_ = this.ability_; 

        //abilityCooldownComponent_ = obj_in.GetComponent<AbilityCooldown>();
        //this.ability_ = abilityCooldownComponent_.ability_;
        //this.name_ = ability_.name_;
        //this.sprite_ = ability_.sprite_;

    }

    public override void UseUpgrade()
    {
        getAbility_.DevilTrigger();
    }
}
