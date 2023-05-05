using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Stay : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Button_Mgr.Instance.dd();
        Debug.Log(gameObject.GetComponent<Button>().spriteState.highlightedSprite);
        if (gameObject.GetComponent<Button>().spriteState.highlightedSprite == null)
            gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<Button>().spriteState.pressedSprite;
        else
            gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<Button>().spriteState.highlightedSprite;
    }
}
