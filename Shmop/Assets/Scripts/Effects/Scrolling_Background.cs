//script source (tutorial): https://www.youtube.com/watch?v=32EIYs6Z18Q
//Responsible for the script: Ivan

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling_Background : MonoBehaviour
{
    public float bg_speed_ = 0f; //speed of background scrolling
    public Renderer bg_renderer_; //put the mesh renderer here

    [Header("Hyperdrive")]
    private ColorController colour_controller_;
    private bool hyperdriving_;
    [SerializeField] private float hyper_drived_speed_;
    [SerializeField] private float bg_stretch_;
    [SerializeField] private float acceleration_;

    public void SetSpeed(float speed)
    {
        bg_speed_ = speed;
    }
    public void SetStretch(float stretch)
    {
        hyperdriving_ = true;
        bg_renderer_.material.mainTextureScale = new Vector2(bg_stretch_, 1f);
    }

    void Update()
    {
        bg_renderer_.material.mainTextureOffset += new Vector2(bg_speed_ * Time.deltaTime, 0f);
        bg_renderer_.material.color = colour_controller_._currentColour;
        if (hyperdriving_)
        {
            bg_speed_ = Mathf.SmoothStep(bg_speed_, hyper_drived_speed_, acceleration_*  Time.deltaTime);
            bg_renderer_.material.mainTextureScale = Vector2.Lerp(bg_renderer_.material.mainTextureScale, new Vector2(bg_stretch_, 1f), acceleration_ * Time.deltaTime);
        }
    }
}

