using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    
    public GameObject explosion;
    public GameObject game_over;
    public GameObject wall_;
    private GameObject spawn_object_;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        spawn_object_ = GameObject.Find("Main Camera");
        spawn_object_.GetComponent<Spawn_Object>().works_ = false;
        Debug.Log("Hit detected!");
        GameObject e = Instantiate(explosion) as GameObject;
        GameObject f = Instantiate(game_over) as GameObject;
        e.transform.position = transform.position;
        f.transform.position = Camera.main.transform.position;
        Destroy(other.gameObject);
        this.gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<Background_Loop>().speed = 0;
        GameObject.Find("Main Camera").GetComponent<Countdown>().enabled = false;
        Asteroid.isMoving_ = false;
        wall_.GetComponent<BoxCollider2D>().enabled = true;
        
    }

   
}
