using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundPlayer : MonoBehaviour
{
    private static AudioSource _audioSource;
    public static SoundPlayer _soundPlayer;

    [SerializeField] public AudioClip _bossIntro, _bossLoop, _boom, _playerDamage;

    [SerializeField] private static float explosionFrequency = 0.1f;
    
    void Start()
    {
        _soundPlayer = FindObjectOfType<SoundPlayer>();
        _audioSource = _soundPlayer.GetComponent<AudioSource>();
    }

    public static void PlayBossIntroMusic()
    {
        _audioSource.clip = _soundPlayer._bossIntro;
        _audioSource.Play();
    }

    public static void PlayBossMusic()
    {
        _audioSource.clip = _soundPlayer._bossLoop;
        _audioSource.Play();
    }

    public static void PlayOneShot(AudioClip clip)
    {
        if (_audioSource == null) return;
        _audioSource.PlayOneShot(clip, 1f);
    }

    public void PlayerDamge()
    {
        PlayOneShot(_soundPlayer._playerDamage);
    }

    public void PlaySingleExplosion()
    {
        PlayOneShot(_boom);
    }
    public void PlayExplosions(int amount, float interval)
    {
        StartCoroutine(Explosions(amount, interval));
    }

    public IEnumerator Explosions(int amount, float interval)
    {
        for (int i = 0; i < (int)((amount * interval) / explosionFrequency); i++)
        {
            PlaySingleExplosion();
            yield return new WaitForSeconds(explosionFrequency);
        }
    }
}
