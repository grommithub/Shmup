//script source (tutorial): https://www.youtube.com/watch?v=32EIYs6Z18Q
//Responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling_Background : MonoBehaviour
{
    private ColorController colour_controller_;

    public float desired_speed_= 0f;
    public float default_speed_ = 0f;
    public float bg_speed_ = 0f; //speed of background scrolling
    public Renderer bg_renderer_; //put the mesh renderer here
    
    [Header("Hyperdrive")]

    [SerializeField] private float hyper_drived_speed_;
    [SerializeField] private float bg_stretch_;

    private void Start()
    {
        colour_controller_ = FindObjectOfType<ColorController>();
        desired_speed_ = default_speed_;
}
    public void SetNormalValues()
    {
        desired_speed_ = default_speed_;
        bg_renderer_.material.mainTextureScale = new Vector2(1f, 1f);
    }

    public void SetHyperdriveValues()
    {
        desired_speed_ = hyper_drived_speed_;
        bg_renderer_.material.mainTextureScale = new Vector2(bg_stretch_, 1f);
    }

    void Update()
    {
        bg_speed_ = Mathf.Lerp(bg_speed_, desired_speed_, 3f * Time.deltaTime);


        bg_renderer_.material.mainTextureOffset += new Vector2(bg_speed_ * Time.deltaTime, 0f);
        bg_renderer_.material.color = colour_controller_._currentColour;
    }
}
