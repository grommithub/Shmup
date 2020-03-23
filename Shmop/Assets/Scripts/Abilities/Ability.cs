//responsible for the script: Ivan
//source of a tutorial: https://youtu.be/bvRKfLPqQ0Q
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject //class that includes all variables and functions that any ability should contain
{
    public string name_; //ability's name
    public Sprite sprite_; //sprite of an ability icon 
    public AudioClip audioClip_; //sound of an ability when it is activated 
    public float baseCooldown_; //default time of a cooldown

    public abstract void Initialize(GameObject obj_); //get components from the target game objects, set all required references here
    public abstract void TriggerAbility(); //include a function here that contains all actions and things that ability is supposed to execute when called
}
