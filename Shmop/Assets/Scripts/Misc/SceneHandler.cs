using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] protected GameObject ship;
    [SerializeField] private float waitTime;

    Hyperdrive hyperdrive;


    // Start is called before the first frame update
    void Start()
    {
        hyperdrive = FindObjectOfType<Hyperdrive>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ship == null)
        {
            StartCoroutine("LoseScene");
        }

        if(ship.transform.position.x >= transform.position.x)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    IEnumerator LoseScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("LoseScreen");
    }
}
