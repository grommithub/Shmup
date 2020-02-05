using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Mouse : MonoBehaviour
{
    //private float initial_position;
    //private float fixed_spaceship_pos = 4.0f;
    private Vector3 tmp;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(-4, 0, 0);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    { 
        tmp = this.transform.position;
        Vector3 mouse_location_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tmp.y = mouse_location_.y;
        tmp.z = -4;
        //mouse_location_.x = -4;
        //mouse_location_.x = transform.position.x + Camera.main.transform.position.x * Time.deltaTime;
        //this.transform.position = mouse_location_;
        this.transform.position = tmp;
    }
}
