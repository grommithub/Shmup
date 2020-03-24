//responsible for the script: Ivan
//source of a tutorial: https://youtu.be/bvRKfLPqQ0Q
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldown : MonoBehaviour
{
    public TextMeshProUGUI cooldownText_; //numbers on the ability icon that are supposed to show how many seconds left until cooldown's end
    public Image darkMask_; //a dark mask of an icon should be contained here
    public KeyCode activationKey_; //key for triggering the ability. Currently should be choosed manually
    public Ability ability_; //contains the ability that we (currently) should choose manually for the script

    [SerializeField] private GameObject player_; //contains the gameobject (primarily the player) that holds the abilities execution scripts
    private Image iconImage_; 
    private AudioSource abilitySource_; //gets sound source that is used by ability when triggered
    private float cooldownDuration_; 
    private float nextReadyTime_; //contains a time in seconds (since a start of the game) when the ability should become available for use again
    private float cooldownTimeLeft_;  

    // Start is called before the first frame update
    void Start()
    {
        Initialize(ability_, player_);
    }

    private void AbilityReady() //this function hides unnecesary components for the time when abilty is ready for use
    {
        cooldownText_.enabled = false;
        darkMask_.enabled = false;
    } 

    private void Cooldown() //displays visual information about the cooldown and countdowns cd
    {
        cooldownTimeLeft_ -= Time.deltaTime;
        float roundCooldown_ = Mathf.Round(cooldownTimeLeft_);
        cooldownText_.text = roundCooldown_.ToString();
        darkMask_.fillAmount = (cooldownTimeLeft_ / cooldownDuration_);
    }

    public void Initialize(Ability ability_in, GameObject player_in) //setting all required references and variable values
    {
        ability_ = ability_in;
        iconImage_ = GetComponent<Image>();
        abilitySource_ = GetComponent<AudioSource>();
        iconImage_.sprite = ability_.sprite_;
        darkMask_.sprite = ability_.sprite_;
        cooldownDuration_ = ability_.baseCooldown_;
        ability_.Initialize(player_in);
        AbilityReady();
    }

    public void Reinitialize()
    {
        Initialize(ability_, player_);
    }

    private void KeyTriggered() //this function executes actions taken when player activates the ability
    {
        nextReadyTime_ = Time.time + cooldownDuration_;
        cooldownTimeLeft_ = cooldownDuration_;
        cooldownText_.enabled = true;
        darkMask_.enabled = true;

        abilitySource_.clip = ability_.audioClip_;
        abilitySource_.Play();
        ability_.TriggerAbility();
    }
    // Update is called once per frame
    void Update()
    {
        bool cooldownIsComplete = (Time.time > nextReadyTime_);
        if(cooldownIsComplete)
        {
            AbilityReady();
            if(Input.GetKeyDown(activationKey_))
            {
                KeyTriggered();
            }
        }
        else
        {
            Cooldown();
        }
    }
}
