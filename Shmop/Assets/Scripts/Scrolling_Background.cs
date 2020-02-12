//script source (tutorial): https://www.youtube.com/watch?v=32EIYs6Z18Q
//Responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling_Background : MonoBehaviour
{
    public float bg_speed_ = 0f; //speed of background scrolling
    public Renderer bg_renderer_; //put the mesh renderer here

    public void SetSpeed(float speed)
    {
        bg_speed_ = speed;
    }
    public void SetStretch(float stretch)
    {

    }
    void Update()
    {
        bg_renderer_.material.mainTextureOffset += new Vector2(bg_speed_ * Time.deltaTime, 0f); 
    }
}
