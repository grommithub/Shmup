//responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrigger : MonoBehaviour
{
    [SerializeField] private GameObject laser_;
    [SerializeField] private GameObject parent_;
    private GameObject beam_;
    private GameObject target_;
    private EnemyBase enemyComponent_;

    [HideInInspector] public LineRenderer lr_;
    [HideInInspector] public float laserDuration_;
    [HideInInspector] public int laserDamage_;

    // Update is called once per frame
    private void Update()
    {
        lr_.SetPosition(0, this.transform.position);
        RaycastHit2D hit_;
        hit_ = Physics2D.Raycast(transform.position, transform.right);
        if (hit_.collider)
        {
            if(hit_.collider.CompareTag("Enemy"))
            {
                lr_.SetPosition(1, hit_.point);
                target_ = hit_.transform.gameObject;
                enemyComponent_ = target_.GetComponent<EnemyBase>();
                enemyComponent_.TakeDamage(laserDamage_);
            }
        }
        else
        {
            lr_.SetPosition(1, this.transform.right * 500);
        }
    }

    public void DevilTrigger()
    {
        beam_ = Instantiate(laser_);
        beam_.transform.SetParent(parent_.transform);
        lr_ = beam_.GetComponent<LineRenderer>();

        PlayerInput.shootButton = string.Empty;

        StartCoroutine(StartCountdown());

        enabled = true;
        
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(laserDuration_);
        Destroy(beam_);
        PlayerInput.shootButton = "Jump";
        enabled = false;
    }
}
