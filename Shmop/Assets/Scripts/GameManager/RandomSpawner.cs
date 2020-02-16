using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnItem;
    [SerializeField] private float _interval, _xPos;
    private float _maxY, _minY, _minWait, _maxWait, _lastSpawn;

    public bool doneSpawning = true; 

    private int _enemiesToSpawn;
    [SerializeField] internal Transform _enemyParent;

    private void Start()
    {
        doneSpawning = true; //just to make sure

        Camera cam = Camera.main;

        _minY = cam.ScreenToWorldPoint(Vector3.zero).y;
        _maxY = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
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
        doneSpawning = false;
        for(int i = 0; i < _enemiesToSpawn; i++)
        {
            float y = Random.Range(_minY + 5, _maxY - 5);
            GameObject e = Instantiate(_spawnItem, new Vector3(_xPos, y, 0f), Quaternion.identity);
            if(_enemyParent != null) e.transform.SetParent(_enemyParent);
            yield return new WaitForSeconds(_interval);
        }
        doneSpawning = true;
    }
}
