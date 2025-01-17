using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TMP_Text buttonText;

    public void OnPointerEnter(PointerEventData eventData){
        buttonText.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData){
        buttonText.color = Color.black;
    }
}
