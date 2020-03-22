using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private bool isTrue_ = true;
    public float revealTime;
    [TextArea] public string text;
    private char[] letters;
    public float buttonWaitTime;
    private bool textSkip;

    // Start is called before the first frame update
    void Start()
    {
        letters = text.ToCharArray();
        button.SetActive(false);
        StartCoroutine(SpawnText());
    }
    IEnumerator SpawnText()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < letters.Length; i++)
        {
            textMeshPro.text += letters[i];
            if (textSkip == true)
            {
                textMeshPro.text = text;
                break;
            }
            yield return new WaitForSeconds(revealTime);
        }

        yield return new WaitForSeconds(buttonWaitTime);
        button.SetActive(true);
        textSkip = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && textSkip == false)
        {
            textSkip = true;
        }
        else if(Input.GetKeyDown(KeyCode.Return) && textSkip == true)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
}

