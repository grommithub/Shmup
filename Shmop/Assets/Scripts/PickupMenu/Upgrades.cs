using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrades : ScriptableObject
{
    public int upgradeType_; //1 is ability unlock, 2 is ability upgrade, 3 is player upgrade
    public string name_; //upgrade's name
    public string description_; //upgrade's description: specs, key assigned and short description
    public Sprite sprite_; //sprite of an icon 

    [HideInInspector] public int index_;
    [HideInInspector] public bool upgradeIsPicked_ = false;
    [HideInInspector] public bool abilityIsUnlocked_ = false;

    public abstract void Initialize(GameObject obj); //get components from the target game objects, set all required references here

    public abstract void UseUpgrade(); //include a function here that contains all actions and things that should happen when upgrade is chosen
}
