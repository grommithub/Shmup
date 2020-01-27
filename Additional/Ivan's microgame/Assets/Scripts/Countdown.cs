using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Countdown : MonoBehaviour
{
    //public GameObject explosion;
    public float startingTime_ = 5.5f;
    public TextMeshProUGUI text_;
    private GameObject spawn_object_;
    private GameObject spaceship_;
    public GameObject explosion_;
    //public GameObject explosion_sound_;
    public GameObject victory_;
    private bool instantiate_victory_condition_ = true;

    // Start is called before the first frame update
    void Start()
    {
        text_.text = startingTime_.ToString();
        spaceship_ = GameObject.Find("Spaceship");
    }

    // Update is called once per frame
    void Update()
    {
        if (startingTime_ > 0)
        {
            startingTime_ -= Time.deltaTime;
            //Debug.Log(startingTime_);
        }

        text_.text = Mathf.Round(startingTime_).ToString();

        if (startingTime_ <= 0)
        {
            spawn_object_ = GameObject.Find("Main Camera");
            spawn_object_.GetComponent<Spawn_Object>().works_ = false;

            spaceship_.GetComponent<AudioSource>().enabled = false;

            var asteroids_ = GameObject.FindGameObjectsWithTag("Asteroid_obj_");
            foreach (var asteroid in asteroids_)
            {
                GameObject e = Instantiate(explosion_) as GameObject;
                e.transform.position = asteroid.transform.position;
                Destroy(asteroid);
            }

            //GameObject g = Instantiate(explosion_sound_) as GameObject;
            //g.transform.position = Camera.main.transform.position;

            if (instantiate_victory_condition_ == true)
            { 
                GameObject f = Instantiate(victory_) as GameObject;
                f.transform.position = Camera.main.transform.position;
                instantiate_victory_condition_ = false;
            }

            StartCoroutine(RestartGame());
        }
    }

    IEnumerator RestartGame()
    {
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("SampleScene");
    }
}
