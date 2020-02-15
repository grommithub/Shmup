using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    private RandomSpawner _spawner;


    private void Start()
    {
        _spawner = GetComponent<RandomSpawner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SpawnWave(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SpawnWave(1);
    }

    private void SpawnWave(int index)
    {
        if (index < 0) return;
        index = Mathf.Min(index, _waves.Count - 1);
        Wave w = _waves[(int)index];
        _spawner.SpawnWave(w.enemy, w.amount, w.interval);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject enemy;
    public int amount;
    public float interval;
}
