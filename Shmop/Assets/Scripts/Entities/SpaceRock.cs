using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRock : EnemyBase
{

    [SerializeField] private float _deviation, _maxDeviation, _spinSpeed;

    protected override void Start()
    {
        _deviation = Random.Range(-_maxDeviation, _maxDeviation);
        _movementVector = Quaternion.AngleAxis(_deviation, Vector3.forward) * Vector2.left;
        _spinSpeed *= _deviation;
        base.Start();
    }

    private void Update()
    {
        Move();
        transform.Rotate(new Vector3(0f, 0f, 1f) * _spinSpeed * Time.deltaTime, Space.World);
        if (transform.position.x < -10f) Destroy(gameObject);
    }
}
