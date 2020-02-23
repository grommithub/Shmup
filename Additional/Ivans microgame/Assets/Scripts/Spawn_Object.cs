using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Object : MonoBehaviour
{
    public GameObject asteroid;
    private Vector2 ScreenBoundaries_;
    private float ObjectHeight_;
    private int counter_high_ = 0;
    private int counter_low_ = 0;
    private float y_coord_;
    public float RespawnTimeLow = 0.2f;
    public float RespawnTimeHigh = 0.7f;
    public bool works_ = true;

    // Start is called before the first frame update
    void Start()
    {
        ScreenBoundaries_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(asteroidWave());
        ObjectHeight_ = asteroid.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    private void SpawnObject()
    {
        GameObject a = Instantiate(asteroid) as GameObject;
        a.transform.position = new Vector2(ScreenBoundaries_.x * 2, y_coord_);
        a.name = "Asteroid_clone";
        if ((-ScreenBoundaries_.y + ObjectHeight_) <= y_coord_ && y_coord_ < 0)
        {
            counter_low_++;
            if (counter_low_ == 3)
            {
                counter_low_ = 0;
                Invoke("SpawnObjectHighHalf", 0.2f);
            }
        }
        else
        {
            counter_high_++;
            if (counter_high_ == 3)
            {
                counter_high_ = 0;
                Invoke("SpawnObjectLowHalf", 0.2f);
            }
        }

    } 

    private void SpawnObjectLowHalf()
    {
        GameObject a = Instantiate(asteroid) as GameObject;
        a.name = "Asteroid_clone";
        a.transform.position = new Vector2(ScreenBoundaries_.x * 2, Random.Range((-ScreenBoundaries_.y + ObjectHeight_), (0)));
    }

    private void SpawnObjectHighHalf()
    {
        GameObject a = Instantiate(asteroid) as GameObject;
        a.name = "Asteroid_clone";
        a.transform.position = new Vector2(ScreenBoundaries_.x * 2, Random.Range((0), (ScreenBoundaries_.y - ObjectHeight_)));
    }

    IEnumerator asteroidWave()
    {
        while (this.works_)
        {
            ScreenBoundaries_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            yield return new WaitForSeconds(Random.Range(RespawnTimeLow, RespawnTimeHigh));
            y_coord_ = Random.Range((-ScreenBoundaries_.y + ObjectHeight_), (ScreenBoundaries_.y - ObjectHeight_));
            SpawnObject();
        }
    }

}
