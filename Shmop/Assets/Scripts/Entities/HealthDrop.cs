using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [SerializeField]private int _healthAmount;
    [SerializeField] private AudioClip pickupClip;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime);
        if (transform.position.x < -10) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.transform.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            player.GiveHealth(_healthAmount);
            SoundPlayer.PlayOneShot(pickupClip);
            Destroy(this.gameObject);
        }
    
    }
}
