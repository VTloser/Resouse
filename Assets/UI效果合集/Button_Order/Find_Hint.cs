using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Find_Hint : MonoBehaviour
{

    public GameObject vertical;    //垂直
    public GameObject Horizontal;  //水平

    public GameObject JiaoJI;

    public GameObject Tag;

    int Max_X = Screen.width;
    int Max_Y = Screen.height;

    bool Moving;
    void Start()
    {

        Vector3 Dp = Camera.main.WorldToScreenPoint(Tag.transform.position);

        Debug.Log(Dp.x);
        Debug.Log(Dp.y);
        vertical.GetComponent<RectTransform>().DOAnchorPos3DX(Random.Range(0, Max_X), 1).SetEase(Ease.Linear).OnComplete(() =>
         {
             vertical.GetComponent<RectTransform>().DOAnchorPos3DX(Random.Range(0, Max_X), 1).SetEase(Ease.Linear).OnComplete(() =>
             {
                 vertical.GetComponent<RectTransform>().DOAnchorPos3DX(Dp.x, 1).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Moving = true;
                });
             });
         });
        Horizontal.GetComponent<RectTransform>().DOAnchorPos3DY(Random.Range(0, Max_Y), 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            Horizontal.GetComponent<RectTransform>().DOAnchorPos3DY(Random.Range(0, Max_Y), 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                Horizontal.GetComponent<RectTransform>().DOAnchorPos3DY(Dp.y, 1).SetEase(Ease.Linear).OnComplete(() =>
               {
                   Moving = true;
               });
            });
        });
    }


    // Update is called once per frame
    void Update()
    {
        if (!Moving)
        {
            JiaoJI.transform.localPosition = new Vector3(vertical.GetComponent<RectTransform>().localPosition.x, Horizontal.GetComponent<RectTransform>().localPosition.y, 0);
        }
    }



}
