using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

//Kill me

public class TextBox : MonoBehaviour
{
    [SerializeField] private GameObject button_;
    [SerializeField] private GameObject textHolder_;
    [SerializeField] private GameObject inActivePosition_;
    [SerializeField] private GameObject activePosition_;
    [SerializeField] private UnityEvent preIntroductionState_;
    [SerializeField] private UnityEvent postIntroductionState_;
    [SerializeField] private float moveTime_;
    private bool firstTime_;
    //private bool isTutorial_;
    private char[] letters_;
    private int arraySize_;
    private int i; //index of current text used
    private Vector3 activePos_;
    private Vector3 inActivePos_;
    private Rigidbody2D rb2D_;
    private TextMeshProUGUI textMeshPro_;

    public float revealTime_;
    public float delay_;
    public float buttonWaitTime_;
    public bool inHyperDrive_;
    [TextArea] public string[] text_;

    void Start()
    {
        preIntroductionState_.Invoke();
        button_.SetActive(false);

        rb2D_ = GetComponent<Rigidbody2D>();
        inActivePos_ = inActivePosition_.transform.position;
        activePos_ = activePosition_.transform.position;
        arraySize_ = text_.Length;
        firstTime_ = true;
        //isTutorial_ = true;
        inHyperDrive_ = false;
        i = 0;

        StartCoroutine(BoxMovement(activePos_, true));
    }

    //private void Update()
    //{
    //    if(inHyperDrive_)
    //    {
    //        StartCoroutine(BoxMovement(activePos_, true));
    //    }
    //}

    /*This method gets a point of a final destination as an argument (vector3 end_) and also 
     takes statement whether the text box is moving out ON or OFF of screen (True stands for ON)*/
    IEnumerator BoxMovement(Vector3 end_, bool isActive_) 
    {
        if(firstTime_)
        {
            yield return new WaitForSeconds(delay_);
            firstTime_ = false;
        }

        float sqrRemainingDistance_ = (transform.position - end_).sqrMagnitude; //Calculates the distance between current position and destination and takes the sqr root out of it. It is claimed that taking root is computationally cheaper rather than not doing it

        while (sqrRemainingDistance_ > float.Epsilon) //This loop will continue until the distance is equal to zero
        {
            Vector3 newPosition_ = Vector3.MoveTowards(rb2D_.position, end_, moveTime_ * Time.deltaTime); //This vector calculates to what position to move per each call of a loop
            rb2D_.MovePosition(newPosition_); //Speaks for itself
            sqrRemainingDistance_ = (transform.position - end_).sqrMagnitude; //Recalculating sqr root of remaining distance
            yield return null; //Pauses function, waits for a frame update, then continues
        }

        if (isActive_)
        {
            Debug.Log(i);
            preIntroductionState_.Invoke();
            StartCoroutine(SpawnText(i));
        }
        else
        {
            inHyperDrive_ = false;
            ClearText();
            postIntroductionState_.Invoke();
            i++;
            //if(isTutorial_)
            //{

            //}
        }
    }

    IEnumerator SpawnText(int index)
    {
        if(index > arraySize_)
        {
            Debug.Log("Error: text_ array (TextBox.cs) index is out of range");
        }

        textMeshPro_ = textHolder_.GetComponent<TextMeshProUGUI>();

        letters_ = text_[index].ToCharArray();

        for (int i = 0; i < letters_.Length; i++)
        {
            textMeshPro_.text += letters_[i];
            yield return new WaitForSeconds(revealTime_);
        }

        yield return new WaitForSeconds(buttonWaitTime_);
        button_.SetActive(true);
    }

    public void MoveBack()
    {
        StartCoroutine(BoxMovement(inActivePos_, false));
    }

    public void MoveOut()
    {
        StartCoroutine(BoxMovement(activePos_, true));
    }

    private void ClearText()
    {
        textMeshPro_.text = string.Empty;
    }
}
