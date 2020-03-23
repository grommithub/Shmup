using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

    [SerializeField] private Sprite[] _healthSprites = new Sprite[10];
    private Image _img;
    private PlayerBehaviour _player;


    private void Start()
    {
        _player = FindObjectOfType<PlayerBehaviour>();
        _img = GetComponent<Image>();
    }

    void Update()
    {
        if(_player == null)
        {
            _img.sprite = _healthSprites[_healthSprites.Length - 1];
            return;
        }

        int index = 10 - _player.Health;

        index = Mathf.Max(0, index);
        index = Mathf.Min(_healthSprites.Length -1, index);
        _img.sprite = _healthSprites[index];
    }
}
