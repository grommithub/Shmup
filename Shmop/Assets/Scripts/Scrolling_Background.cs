//script source (tutorial): https://www.youtube.com/watch?v=32EIYs6Z18Q
//Responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling_Background : MonoBehaviour
{
    private ColorController colour_controller_;

    public float bg_speed_ = 0f; //speed of background scrolling
    public Renderer bg_renderer_; //put the mesh renderer here
    
    [Header("Hyperdrive")]

    [SerializeField] private float hyper_drived_speed_;
    [SerializeField] private float bg_stretch_;

    private void Start()
    {
        colour_controller_ = FindObjectOfType<ColorController>();
    }

    public void SetHyperdriveValues()
    {
        bg_speed_ = hyper_drived_speed_;
        bg_renderer_.material.mainTextureScale = new Vector2(bg_stretch_, 1f);
    }

    void Update()
    {
        bg_renderer_.material.mainTextureOffset += new Vector2(bg_speed_ * Time.deltaTime, 0f);
        bg_renderer_.material.color = colour_controller_._currentColour;
    }
}
