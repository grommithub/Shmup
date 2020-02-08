using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [SerializeField]private int _healthAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.transform.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            player.GiveHealth(_healthAmount);
            Destroy(this.gameObject);
        }
    
    }
}
