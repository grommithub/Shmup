using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInteraction : MonoBehaviour
{
    public TextMeshProUGUI textHolder_;
    public string[] buttonsDescription_;
    [HideInInspector] public int rng_;
    public List<Upgrades> upgrades_; //contains the ability that we (currently) should choose manually for the script

    [SerializeField] private GameObject[] buttons_; //contains the ability that we (currently) should choose manually for the script
    //[SerializeField] private GameObject player_; //contains the gameobject (primarily the player) that holds the abilities execution scripts
    private Image iconImage_;
    private UpgradeIconHandler upgradeIconHandlerComponent_;
    private int optionsAmount_;
    private int poolSize_;
    private bool laserIsUnlocked_;
    private bool havocIsUnlocked_;
    private bool shieldIsUnlocked_;
    private int[] bannedNumbers_ = { };

    //ew

    // Start is called before the first frame update
    void Start()
    {
        laserIsUnlocked_ = false;
        havocIsUnlocked_ = false;
        shieldIsUnlocked_ = false;
        optionsAmount_ = buttons_.Length - 1;
        poolSize_ = upgrades_.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignUpgrades()
    {
        poolSize_ = upgrades_.Count - 1;
        for (int i = 0; i <= poolSize_; i++)
        {
            upgrades_[i].upgradeIsPicked_ = false;
            upgrades_[i].index_ = 0;
        }
        for (int i = 0; i <= optionsAmount_; i++)
        {
            int rng = Random.Range(0, poolSize_ + 1);
            bool isSuitable = false;

            while(upgrades_[rng].upgradeIsPicked_ || !isSuitable)
            {
                rng = Random.Range(0, poolSize_ + 1);
                if (upgrades_[rng].upgradeType_ == 2)
                {
                    if (upgrades_[rng].name_ == "Upgraded Havoc")
                    {
                        if (!havocIsUnlocked_)
                        {
                            isSuitable = false;
                        }
                        else
                        {
                            isSuitable = true;
                        }
                    }
                    else if (upgrades_[rng].name_ == "Upgraded Laser")
                    {
                        if (!laserIsUnlocked_)
                        {
                            isSuitable = false;
                        }
                        else
                        {
                            isSuitable = true;
                        }
                    }
                    else if (upgrades_[rng].name_ == "Upgraded Shield")
                    {
                        if (!shieldIsUnlocked_)
                        {
                            isSuitable = false;
                        }
                        else
                        {
                            isSuitable = true;
                        }
                    }
                }
                else
                {
                    isSuitable = true;
                }                
            }

            Debug.Log(rng);
            upgrades_[rng].upgradeIsPicked_ = true;
            Initialize(upgrades_[rng], buttons_[i], rng);
        }
    }

    public void DeleteFromPool(int index)
    {
        if (upgrades_[index].upgradeType_ == 1)
        {
            if (upgrades_[index].name_ == "Havoc")
            {
                havocIsUnlocked_ = true;
            }
            else if (upgrades_[index].name_ == "Laser")
            {
                laserIsUnlocked_ = true;
            }
            else if (upgrades_[index].name_ == "Shield")
            {
                shieldIsUnlocked_ = true;
            }
        }
        upgrades_.RemoveAt(index);
    }


    private void Initialize(Upgrades upgrade_in, GameObject button_in, int index) //setting all required references and variable values
    {
        iconImage_ = button_in.GetComponent<Image>();
        upgradeIconHandlerComponent_ = button_in.GetComponent<UpgradeIconHandler>();
        iconImage_.sprite = upgrade_in.sprite_;
        string text = "Name: " + upgrade_in.name_ + "\n" + "Description: " + upgrade_in.description_;
        upgradeIconHandlerComponent_.description_ = text;
        upgradeIconHandlerComponent_.upgrade_ = upgrade_in;
        upgrade_in.index_ = index;
    }
}
