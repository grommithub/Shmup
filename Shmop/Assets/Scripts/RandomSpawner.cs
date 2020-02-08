using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnItem;
    [SerializeField] private float interval, xPos;
    private float maxY, minY, minWait, maxWait, lastSpawn;

    private void Start()
    {
        Camera cam = Camera.main;

        minY = cam.ScreenToWorldPoint(Vector3.zero).y;
        maxY = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
    }

    private void Update()
    {
        if(Time.time > lastSpawn + interval)
        {
            float y = Random.Range(minY + 3, maxY - 3);
            Instantiate(spawnItem, new Vector3(xPos, y, 0f), Quaternion.identity);
            lastSpawn = Time.time;
        }
    }
}
