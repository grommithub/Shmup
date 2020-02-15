using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnItem;
    [SerializeField] private float _interval, _xPos;
    private float _maxY, _minY, _minWait, _maxWait, _lastSpawn;

    private int _enemiesToSpawn;
    [SerializeField] internal Transform _enemyParent;

    private void Start()
    {
        Camera cam = Camera.main;

        _minY = cam.ScreenToWorldPoint(Vector3.zero).y;
        _maxY = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
    }

    private void Update()
    {
        //if(Time.time > _lastSpawn + _interval)
        //{
        //    float y = Random.Range(_minY + 3, _maxY - 3);
        //    Instantiate(_spawnItem, new Vector3(_xPos, y, 0f), Quaternion.identity);
        //    _lastSpawn = Time.time;
        //}
    }

    public void SpawnWave(GameObject enemy, int amount, float interval)
    {
        _spawnItem = enemy;
        _enemiesToSpawn = amount;
        _interval = interval;

        StartCoroutine("TimedSpawning");
    }

    private IEnumerator TimedSpawning()
    {
        for(int i = 0; i < _enemiesToSpawn; i++)
        {
            float y = Random.Range(_minY + 3, _maxY - 3);
            GameObject e = Instantiate(_spawnItem, new Vector3(_xPos, y, 0f), Quaternion.identity);
            //e.transform.SetParent()
            yield return new WaitForSeconds(_interval);
        }
    }

}
