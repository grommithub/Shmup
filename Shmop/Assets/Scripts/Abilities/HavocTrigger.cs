//responsible for the script: Ivan
//tutorial took as a reference: https://www.youtube.com/watch?v=-MZD_dZ41rI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavocTrigger : MonoBehaviour
{
    private bool isShooting_; 
    private bool isShaking_; //boolean storing value about whether the object SHOULD shake in the upcoming frame. So the each time shaking method is called, the variables is set to false
    private int originDamage_;
    private int finalDamage_; //argument that would be passed to the target projectile prefab at the end
    private Rigidbody2D rb2D_; //component reference
    private ProjectileBase projectileBase_;


    //the variables which get their values from corresponding ones in Ability script
    [HideInInspector] public float havocDuration_; 
    [HideInInspector] public float recoilForce_;
    [HideInInspector] public float shakeForce_;
    [HideInInspector] public int damageMultiplier_;
    [HideInInspector] public GameObject projectile_;

    //Activates the script and also launches countdown for its deactivation. Only called externally
    public void DevilTrigger()
    {
        StartCoroutine(StartCountdown());

        finalDamage_ = originDamage_ * damageMultiplier_;
        projectileBase_.damage = finalDamage_;

        enabled = true;
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(havocDuration_);
        enabled = false;
        projectileBase_.damage = originDamage_;
    }

    private void Shake()
    {
        Vector2 newPos_ = transform.position + (Random.insideUnitSphere * (Time.deltaTime * shakeForce_)); //Generating a random vector and using it as a delta for current object's position
        transform.position = newPos_;
        isShaking_ = false;
    }

    private void GitBacked()
    {
        Debug.Log("isShooting_ = " + isShooting_);
        isShaking_ = true;
        rb2D_.MovePosition(new Vector2(transform.position.x, transform.position.y) - (rb2D_.velocity * recoilForce_ * Time.deltaTime));
    }

    //setting starting values of boolean variables and getting references
    private void Awake() 
    {
        enabled = false;
        isShaking_ = false;
        isShooting_ = false;
        rb2D_ = GetComponent<Rigidbody2D>();
        projectileBase_ = projectile_.GetComponent<ProjectileBase>();
        originDamage_ = projectileBase_.damage;
    }


    private void Update()
    {
        isShooting_ = Input.GetButton(PlayerInput.shootButton);

        if (isShaking_)
        {
            Shake();
        }
    }

    private void FixedUpdate()
    {
        if (isShooting_)
        {
            GitBacked();
        }
    }
}
