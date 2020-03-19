using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{

    private Vector2 _startPosition;
    [SerializeField] private float _bobSpeed = 3f;
    private SpriteRenderer _sr;

    private void Start()
    {
        _startPosition = transform.localPosition;
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.localPosition = _startPosition + new Vector2(0f, (Mathf.Sin(Time.time * _bobSpeed) + 1) * 0.1f);
        _sr.flipX = (Mathf.Cos(Time.time * _bobSpeed) > 0f);
    }
}
