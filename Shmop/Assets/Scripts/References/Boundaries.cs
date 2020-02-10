//script source (tutorial): https://www.youtube.com/watch?v=ailbszpt_AI
//Responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 ScreenBoundaries_;
    private float ObjectWidth_;
    private float ObjectHeight_;

    // Start is called before the first frame update
    void Start()
    {
        ScreenBoundaries_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        ObjectWidth_ = transform.GetComponent<SpriteRenderer>().bounds.size.x / 4;
        ObjectHeight_ = transform.GetComponent<SpriteRenderer>().bounds.size.y / 4;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 ViewPos_ = transform.position;
        ViewPos_.x = Mathf.Clamp(ViewPos_.x, -ScreenBoundaries_.x + ObjectWidth_, ScreenBoundaries_.x - ObjectWidth_);
        ViewPos_.y = Mathf.Clamp(ViewPos_.y, -ScreenBoundaries_.y + ObjectHeight_, ScreenBoundaries_.y - ObjectHeight_);
        Debug.Log(ScreenBoundaries_);
        transform.position = ViewPos_;
    }
}
