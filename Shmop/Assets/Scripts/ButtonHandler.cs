using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonHandler : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
