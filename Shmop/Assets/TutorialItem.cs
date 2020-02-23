using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialItem : MonoBehaviour
{
    private Animator anim; 

    [SerializeField] private KeyCode button;

    [SerializeField] private UnityEvent keyDown;
    [SerializeField] private UnityEvent keyUp;

    [SerializeField] private GameObject explosion;

    [SerializeField] private int explosions;
    [SerializeField] private Vector2 maxExplosionDistance;

    private Vector2 _direction;

    [SerializeField] private Key key;
    private bool _moving;
    [SerializeField] private float _speed, _killWait, _killTime;


    private enum Key
    {
        up,
        down,
        left,
        right,
        shoot
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        keyDown.AddListener(Click);
        keyUp.AddListener(UnClick);

        switch(key)
        {
            case Key.left:
                button = KeyCode.LeftArrow;
                _direction = Vector2.left;
                break;
            case Key.right:
                button = KeyCode.RightArrow;
                _direction = Vector2.right;
                break;
            case Key.down:
                button = KeyCode.DownArrow;
                _direction = Vector2.down;
                break;
            case Key.up:
                button = KeyCode.UpArrow;
                _direction = Vector2.up;
                break;
            case Key.shoot:
                button = KeyCode.Space;
                _direction = Vector2.right;
                _killTime = 0f;
                break;

            default:
                break;
        }
    }

    private void Click()
    {
        anim.SetTrigger("Click");
    }
    private void UnClick()
    {
        anim.SetTrigger("UnClick");
    }

    private void Explode()
    {
        for (int i = 0; i < explosions; i++)
        {
            float x, y, s;
            x = Random.Range(-maxExplosionDistance.x, maxExplosionDistance.x);
            y = Random.Range(-maxExplosionDistance.y, maxExplosionDistance.y);
            s = Random.Range(0.75f, 1.25f);

            Vector3 offset = new Vector3(x, y, 0f);

            GameObject expl = Instantiate(explosion, transform.position + offset, Quaternion.identity);
            expl.transform.localScale = new Vector3(s, s, 1f);
            expl.GetComponent<AnimationDelay>().SetWaitTime(0.05f * i);
            expl.transform.localScale *= maxExplosionDistance.y;

            Destroy(expl, 5f);
        }
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(button))
        {
            keyDown.Invoke();

        }
        if(Input.GetKeyUp(button))
        {
            keyUp.Invoke();
            _moving = true;
            _killTime = Time.time + _killWait;
           
        }

        if (_moving == true)
        {
            transform.Translate(_direction * _speed * Time.deltaTime);

            if(Time.time > _killTime)
            {
                Explode();
            }
            
        }
    }
}
