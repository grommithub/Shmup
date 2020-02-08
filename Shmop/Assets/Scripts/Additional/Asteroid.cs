using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] public static float speed = 10.0f;
    public Rigidbody2D rb;
    private Vector2 ScreenBoundaries_;
    public static bool isMoving_;
    private GameObject spawn_object_;
    public List<Sprite> AllSprites;

    void SetSpeed(float speed_in)
    {
        speed = speed_in;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr) sr.sprite = AllSprites[Random.Range(0, AllSprites.Count)];
        isMoving_ = true;
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        ScreenBoundaries_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving_)
        {
            rb.simulated = false;
        }
        else
            rb.velocity = new Vector2(-speed, 0);
        ScreenBoundaries_ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < (ScreenBoundaries_.x * 0.05))
            Destroy(this.gameObject);
    }
}
