using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/PlayerUpgrade")]
public class PlayerUpgrade : Upgrades
{
    public int maxHealth_;
    public float firerate_;
    public float speed_;

    private UpgradePlayer upgradePlayer_;

    public override void Initialize(GameObject obj_in)
    {
        upgradePlayer_ = obj_in.GetComponent<UpgradePlayer>();

        upgradePlayer_.maxHealth_ = this.maxHealth_;
        upgradePlayer_.firerate_ = this.firerate_;
        upgradePlayer_.speed_ = this.speed_;
        upgradePlayer_.name_ = this.name_;
    }

    public override void UseUpgrade()
    {
        upgradePlayer_.DevilTrigger();
    }
}