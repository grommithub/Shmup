using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 ScreenBoundaries_;
    private float ObjectWidth_;
    private float ObjectHeight_;


    void Start()
    {
        ScreenBoundaries_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        ObjectWidth_ = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        ObjectHeight_ = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 ViewPos_ = transform.position;
        //ViewPos_.x = Mathf.Clamp(ViewPos_.x, -ScreenBoundaries_.x + ObjectWidth_, ScreenBoundaries_.x - ObjectWidth_);
        ViewPos_.y = Mathf.Clamp(ViewPos_.y, -ScreenBoundaries_.y + ObjectHeight_, ScreenBoundaries_.y - ObjectHeight_);
        transform.position = ViewPos_;
    }
}
