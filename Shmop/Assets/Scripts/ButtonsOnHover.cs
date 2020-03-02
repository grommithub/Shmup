using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonsOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject textHolder_; //put the child object of the button (text) here
    private TextMeshProUGUI text_;

    private void Start()
    {
        text_ = textHolder_.GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text_.color = Color.red;
        text_.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.white);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text_.color = Color.white;
        text_.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.black);
    }
}
