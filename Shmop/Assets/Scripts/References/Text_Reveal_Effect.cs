using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Reveal_Effect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_mesh_pro_;
    private bool is_true_ = true;
    public float reveal_time_ = 0.25f;
    [TextArea] public string text_ = "";
    private char[] letters_;

    // Start is called before the first frame update
    void Start()
    {
        letters_ = text_.ToCharArray();
        StartCoroutine(SpawnText());
    }
    IEnumerator SpawnText()
    {
        text_mesh_pro_ = GetComponent<TextMeshProUGUI>();// ?? gameObject.AddComponent<TextMeshPro>();

        for(int i = 0; i < letters_.Length; i++)
        {
            text_mesh_pro_.text += letters_[i];
            yield return new WaitForSeconds(reveal_time_);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
