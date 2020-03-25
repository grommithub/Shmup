using UnityEngine;

public class Wobble : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * 5) * 5f);       
    }
}
