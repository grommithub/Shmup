using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Scene : MonoBehaviour
{
    private Animation explosion;
    bool animation_is_played;
    string name_;
   
    // Start is called before the first frame update
    void Start()
    {
        name_ = GetComponent<Animation>().name;
        explosion = GetComponent<Animation>();
        if (explosion == null)
        {
            Debug.Log("Error: Did not find animation!");
        }
        else
        {
            //Debug.Log("Got anim");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //animation_is_played = GetComponent<Animation>().IsPlaying(name_);
        //if (!animation_is_played)
            StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("SampleScene");
    }
}
