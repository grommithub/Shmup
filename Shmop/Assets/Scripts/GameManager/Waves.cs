using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<List<Wave>> levels = new List<List<Wave>>();
    private RandomSpawner _spawner;
    private int _waveNum = 0;
    private bool _spawning = true;
    private bool _waiting;

    [SerializeField] private float _pauseTime;
    private float _nextSpawn;
    private void Start()
    {
        _nextSpawn = _pauseTime + Time.time;
        _spawner = GetComponent<RandomSpawner>();
    }

    private void Update()
    {
        if (Time.time < _nextSpawn) return;

        if (_spawner.doneSpawning && _spawning)
        {
            if (_spawner._enemyParent == null) return;

            if (_spawner._enemyParent.childCount <= 0)
            {
                if(_waiting && Time.time > _nextSpawn)
                {
                    _nextSpawn = Time.time + _pauseTime;
                    _waiting = false;

                }
                if(Time.time > _nextSpawn && !_waiting)
                {
                    _spawning = SpawnWave(_waveNum);
                    _waveNum++;
                    _waiting = true;
                }
            }
        }
    }

    private bool SpawnWave(int index)
    {
        if (index < 0) return false;

        _nextSpawn = _pauseTime + Time.time;

        if(index > _waves.Count - 1)
        {
            print("All waves spawned");
            GameObject.FindObjectOfType<Hyperdrive>().StartHyperDrive();
            return false;
        }
        print("spawn");
        Wave w = _waves[index];
        _spawner.SpawnWave(w.enemy, w.amount, w.interval);
        return true;
    }

}

[System.Serializable]
public class Wave
{
    public GameObject enemy;
    public int amount;
    public float interval;
}
