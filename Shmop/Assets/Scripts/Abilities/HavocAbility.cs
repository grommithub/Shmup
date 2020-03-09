//responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//test
[CreateAssetMenu(menuName = "Abilities/Havoc")] //creating an option to create such asset in unity menu
public class HavocAbility : Ability
{
    private HavocTrigger havoc_;

    public float havocDuration_;    //Ability duration in seconds
    public float recoilForce_;  //Responsible for making random look less random; a multiplier for Vector that is oppositly directed to the Velocity. Recommended values are [1; 10]
    public float shakeForce_; //Speaks for itself. Recommended values are [10; 100]
    public int damageMultiplier_; //Although it is float, for now it is recommended to set integer values because health and damage variables are int
    public GameObject projectile_; //Should store the prefab of projectile which damage should get increased

    public override void Initialize(GameObject obj_in) //Passing the values set in the scriptable object to the Trigger Script
    {
        havoc_ = obj_in.GetComponent<HavocTrigger>();

        havoc_.havocDuration_ = this.havocDuration_;
        havoc_.recoilForce_ = this.recoilForce_;
        havoc_.shakeForce_ = this.shakeForce_;
        havoc_.damageMultiplier_ = this.damageMultiplier_;
        havoc_.projectile_ = this.projectile_;
    }

    public override void TriggerAbility()
    {
        havoc_.DevilTrigger(); //Launching Trigger script
    }
}
