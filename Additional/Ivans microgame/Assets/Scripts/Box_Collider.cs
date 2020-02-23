using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Collider : MonoBehaviour
{
    private Vector3 tmp_;
    private Vector3 camera_;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = new Vector3(-4, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        camera_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        tmp_ = camera_;
        tmp_.y = 0;
        tmp_.x = camera_.x * 1.1f;
        //mouse_location_.x = -4;
        //mouse_location_.x = transform.position.x + Camera.main.transform.position.x * Time.deltaTime;
        //this.transform.position = mouse_location_;
        this.transform.position = tmp_;
    }
}
