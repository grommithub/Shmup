using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    private Weapon weapon_;
    private PlayerBehaviour playerBehaviour_;

    [HideInInspector] public int maxHealth_;
    [HideInInspector] public float firerate_;
    [HideInInspector] public float speed_;
    [HideInInspector] public string name_;

    public void DevilTrigger()
    {
        weapon_ = GetComponent<Weapon>();
        playerBehaviour_ = GetComponent<PlayerBehaviour>();

        if (name_ == "Firerate Upgrade")
        {
            Debug.Log("firerate_: " + weapon_._shootSpeed);
            weapon_._shootSpeed = firerate_;
        }
        else if (name_ == "Movement Speed Upgrade")
        {
            Debug.Log("speed_: " + playerBehaviour_._speed);
            playerBehaviour_._speed = speed_;
        }
        else if (name_ == "Max Health Upgrade")
        {
            Debug.Log("maxHealth_: " + playerBehaviour_._maxHealth);
            playerBehaviour_._maxHealth = maxHealth_;
        }
        else
            Debug.LogError("Error in UpgradePlayer.cs: no proper name was received");
    }
}
