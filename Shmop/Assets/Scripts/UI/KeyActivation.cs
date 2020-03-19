using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyActivation : MonoBehaviour
{
    private Button button_;

    // Start is called before the first frame update
    void Start()
    {
        button_ = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            button_.onClick.Invoke();
        }
    }
}
