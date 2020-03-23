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
    private int[] bannedNumbers_ = { };

    //ew

    // Start is called before the first frame update
    void Start()
    {
        optionsAmount_ = buttons_.Length - 1;
        poolSize_ = upgrades_.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignUpgrades()
    {
        for (int i = 0; i <= poolSize_; i++)
        {
            upgrades_[i].upgradeIsPicked_ = false;
            upgrades_[i].index_ = 0;
        }
        for (int i = 0; i <= optionsAmount_; i++)
        {
            poolSize_ = upgrades_.Count - 1;
            int rng = Random.Range(0, poolSize_ + 1);
            if(upgrades_[rng].upgradeIsPicked_)
            {
                while(upgrades_[rng].upgradeIsPicked_)
                {
                    rng = Random.Range(0, poolSize_ + 1);
                }
            }
            else
            {
                Debug.Log(rng);
                Initialize(upgrades_[rng], buttons_[i], rng);
            }
        }
    }

    public void DeleteFromPool(int index)
    {
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
