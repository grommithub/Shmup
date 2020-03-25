using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSound : MonoBehaviour
{
    private AudioSource audioSource_;

    public AudioClip onHoverSound_;
    public AudioClip onClickSound_;

    private void Start()
    {
        audioSource_ = GetComponent<AudioSource>();
    }

    public void PlayHoverSound_()
    {
        audioSource_.PlayOneShot(onHoverSound_);
    }

    public void PlayClickSound_()
    {
        audioSource_.PlayOneShot(onClickSound_);
    }

}
