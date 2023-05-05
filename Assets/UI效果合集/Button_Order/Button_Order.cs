using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Order : MonoBehaviour, IPointerClickHandler
{

    public List<Button> Buttons = new List<Button>();
    public int Count_Order;

    public Color color;
    public float Interval = 0.2f;
    public float Size = 4;

    void Start()
    {
        Begin_Button_Order();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //print("当前点击的UI是：" + eventData.);
    }

    /// <summary>
    /// 顺序按钮开始
    /// </summary>
    void Begin_Button_Order()
    {
        Count_Order = 0;
        Buttons.ForEach(_ =>
        {
            _.interactable = false;
            _.onClick.AddListener(() => { Function_Order(); });
        });
        Buttons[Count_Order].interactable = true;
        Button_High(Buttons[Count_Order]);
    }

    /// <summary>
    /// 顺序按钮方法
    /// </summary>
    private void Function_Order()
    {
        Buttons[Count_Order].interactable = false;
        StopAllCoroutines();
        Destroy(Buttons[Count_Order++].gameObject.GetComponent<Outline>());
        if (Count_Order >= Buttons.Count)
        {
            Debug.Log("Finish");
            return;
        }
        Buttons[Count_Order].interactable = true;
        Button_High(Buttons[Count_Order]);
    }

    private void Button_High(Button button)
    {
        if (button.gameObject.GetComponent<Outline>() == null)
            button.gameObject.AddComponent<Outline>();
        button.gameObject.GetComponent<Outline>().effectColor = color;
        button.gameObject.GetComponent<Outline>().effectDistance = new Vector2(Size,-Size);
        StartCoroutine(High(button.gameObject.GetComponent<Outline>()));
    }

    IEnumerator High(Outline line)
    {
        if (line)
        {
            while (true)
            {
                {
                    line.enabled = true;
                    yield return new WaitForSeconds(Interval);
                    line.enabled = false;
                    yield return new WaitForSeconds(Interval);
                }
            }
        }
    }
}
