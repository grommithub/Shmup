using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] private GameObject droneInActivePosition_;
    [SerializeField] private GameObject player_;
    [SerializeField] private float delay_;
    [SerializeField] private float moveTime_;
    [SerializeField] private Sprite[] sprites_ = new Sprite[2]; //array of 2 sprites: 1st sprite ([0]) is for the drone before ability is picked, 2nd ([1]) - after)
    private SpriteRenderer spriteRenderer_;

    private Rigidbody2D rb2D_;
    private Vector3 inActivePos_;
    private Vector3 activePos_;


    /*This method gets a point of a final destination as an argument (vector3 end_) and also 
     takes statement whether the object is moving out ON or OFF of screen (True stands for ON)*/
    IEnumerator MovingDrone(Vector3 end_, bool isActive_)
    {
        if(isActive_)
        {
            spriteRenderer_.sprite = sprites_[0];
            yield return new WaitForSeconds(delay_);
        }
        else
        {
            spriteRenderer_.sprite = sprites_[1];
            yield return new WaitForSeconds(delay_);
        }

        float sqrRemainingDistance_ = (transform.position - end_).sqrMagnitude; //Calculates the distance between current position and destination and takes the sqr root out of it. It is claimed that taking root is computationally cheaper rather than not doing it

        while (sqrRemainingDistance_ > float.Epsilon) //This loop will continue until the distance is equal to zero
        {
            Vector3 newPosition_ = Vector3.MoveTowards(rb2D_.position, end_, moveTime_ * Time.deltaTime); //This vector calculates to what position to move per each call of a loop
            rb2D_.MovePosition(newPosition_); //Speaks for itself
            sqrRemainingDistance_ = (transform.position - end_).sqrMagnitude; //Recalculating sqr root of remaining distance
            yield return null; //Pauses function, waits for a frame update, then continues
        }
    }

    public void MoveDroneOut()
    {
        activePos_ = player_.transform.position + new Vector3(0, 1, 0);
        StartCoroutine(MovingDrone(activePos_, true));
    }

    public void MoveDroneBack()
    {
        StartCoroutine(MovingDrone(inActivePos_, false));
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D_ = GetComponent<Rigidbody2D>();
        inActivePos_ = droneInActivePosition_.transform.position;
        spriteRenderer_ = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
