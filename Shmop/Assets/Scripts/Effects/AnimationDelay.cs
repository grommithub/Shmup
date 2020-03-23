using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    public float _waitTime;
    [SerializeField] private float minWait, maxWait;
    private Animator _animator;
    private float _spawnTime;


    public void SetWaitTime(float waitTime)
    {
        _animator = GetComponent<Animator>();
        if (_animator == null) return;
        _animator.enabled = false;

        _waitTime = waitTime;
        _spawnTime = Time.time;
    }
    private void Update()
    {
        if (_animator == null) return;
        if (Time.time > _spawnTime + _waitTime)
        {
            _animator.enabled = true;
            Destroy(gameObject, 1f);
        }
    }
}
