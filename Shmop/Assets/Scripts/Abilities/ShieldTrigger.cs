//responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    private BoxCollider2D bc2D_;
    [SerializeField] private GameObject sprite;

    [HideInInspector] public float shieldDuration_; //the variables in trigger scripts should be hidden because the values for them are going to be set in the asset

    public void DevilTrigger()
    {
        bc2D_ = GetComponent<BoxCollider2D>();
        bc2D_.enabled = false;
        sprite.SetActive(true);
        StartCoroutine(StartCountdown()); //Coroutine allows us to set a desired amount of time for ability to work
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(shieldDuration_);
        bc2D_.enabled = true;
        sprite.SetActive(false);
    }
}

