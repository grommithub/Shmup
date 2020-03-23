using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private void Update()
    {
        if(transform.childCount == 0)
        {
            GameObject.FindObjectOfType<Waves>().enabled = true;
            Destroy(gameObject);
        }
    }
}
