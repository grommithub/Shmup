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
        upgrade_.Initialize(UI_);
        upgrade_.UseUpgrade();
        upgradeInteraction_.DeleteFromPool(upgrade_.index_);
    }

    // Update is called once per frame
    void Update()
    {
        //TMProComponent_.text = string.Empty;
    }
}
