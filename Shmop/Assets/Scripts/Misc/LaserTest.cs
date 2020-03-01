using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTest : MonoBehaviour
{
    [SerializeField] private GameObject laser_;
    [SerializeField] private GameObject parent_;
    private GameObject beam_;
    private LineRenderer lr_;

    // Start is called before the first frame update
    void Start()
    {
        beam_ = Instantiate(laser_);
        beam_.transform.SetParent(parent_.transform);
        lr_ = beam_.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        beam_.transform.position = this.transform.position;
        lr_.SetPosition(0, this.transform.position);
        //lr_.SetPosition(1, this.transform.right * 500);
        RaycastHit2D hit_;
        hit_ = Physics2D.Raycast(transform.position, transform.right);
        if (hit_.collider)
        {
            lr_.SetPosition(1, hit_.point);
        }
        else
        {
            lr_.SetPosition(1, this.transform.right * 500);
        }
    }
}
