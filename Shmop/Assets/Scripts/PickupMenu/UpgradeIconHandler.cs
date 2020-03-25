using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeIconHandler : MonoBehaviour
{
    [HideInInspector] public string description_;
    [HideInInspector] public Upgrades upgrade_;

    [SerializeField] private TextMeshProUGUI TMProComponent_;
    [SerializeField] private GameObject UI_;
    [SerializeField] private GameObject player_;
    [SerializeField] private GameObject abilityBox_;
    private UpgradeInteraction upgradeInteraction_;

    public void DisplayText()
    {
        TMProComponent_.text = description_;
        Debug.Log("Works");
        Debug.Log(TMProComponent_.text);
        Debug.Log(description_);
    }

    // Start is called before the first frame update
    void Start()
    {
        TMProComponent_.text = string.Empty;
        upgradeInteraction_ = abilityBox_.GetComponent<UpgradeInteraction>();
    }

    public void PickAbility()
    {
        if (upgrade_.upgradeType_ == 3)
        {
            upgrade_.Initialize(player_);
        }
        else if (upgrade_.upgradeType_ == 1 || upgrade_.upgradeType_ == 2)
        {
            upgrade_.Initialize(UI_);
        }
        else
            Debug.LogError("UpgradeIconHandler.cs error: no proper upgrade type value was received");

        upgrade_.UseUpgrade();
        upgradeInteraction_.DeleteFromPool(upgrade_.index_);
    }

    // Update is called once per frame
    void Update()
    {
        //TMProComponent_.text = string.Empty;
    }
}
