using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 ScreenBoundaries_;
    private float ObjectWidth_;
    private float ObjectHeight_;

    private Mode mode = Mode.none;

    [SerializeField] private enum Mode
    {
        player,
        enemy,
        bullet,
        none
    }


    void Start()
    {
        if(GetComponent<PlayerBehaviour>() != null)
        {
            mode = Mode.player;
        }
        else if(GetComponent<EnemyBase>() != null)
        {
            mode = Mode.enemy;
        }
        else if (GetComponent<ProjectileBase>() != null)
        {
            mode = Mode.bullet;
        }


        ScreenBoundaries_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        ObjectWidth_ = transform.GetComponent<SpriteRenderer>().bounds.size.x / 4;
        ObjectHeight_ = transform.GetComponent<SpriteRenderer>().bounds.size.y / 4;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 ViewPos_ = transform.position;

        switch(mode)
        {
            case Mode.player:
                {
                    ViewPos_.x = Mathf.Clamp(ViewPos_.x, -ScreenBoundaries_.x + ObjectWidth_, ScreenBoundaries_.x - ObjectWidth_);
                    ViewPos_.y = Mathf.Clamp(ViewPos_.y, -ScreenBoundaries_.y + ObjectHeight_, ScreenBoundaries_.y - ObjectHeight_);
                    transform.position = ViewPos_;
                }
                break;
            case Mode.enemy:
                {
                    ViewPos_.x = Mathf.Clamp(ViewPos_.x, -ScreenBoundaries_.x + ObjectWidth_, ScreenBoundaries_.x  * 1.5f);
                    ViewPos_.y = Mathf.Clamp(ViewPos_.y, -ScreenBoundaries_.y + ObjectHeight_, ScreenBoundaries_.y - ObjectHeight_);
                    transform.position = ViewPos_;
                }
                break;
            case Mode.bullet:
                {
                    ViewPos_.x = Mathf.Clamp(ViewPos_.x, -ScreenBoundaries_.x + ObjectWidth_ - 15f, (ScreenBoundaries_.x * 1.5f) - ObjectWidth_);
                    ViewPos_.y = Mathf.Clamp(ViewPos_.y, -ScreenBoundaries_.y + ObjectHeight_ - 10f, ScreenBoundaries_.y - ObjectHeight_ + 10f);
                    if (transform.position != ViewPos_)
                        Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
 