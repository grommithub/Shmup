using UnityEngine;
public class WiggleParts : MonoBehaviour
{
    [SerializeField] private float _rotationAmount, _rotationSpeed;
    
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * _rotationSpeed) * _rotationAmount);
    }
}
