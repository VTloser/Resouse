﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler {
    private ScrollRect rect;                        //滑动组件  
    private float targethorizontal = 0;             //滑动的起始坐标  
    private bool isDrag = false;                    //是否拖拽结束  
    private List<float> posList = new List<float> ();  //求出每页的临界角，页索引从0开始  
    private int currentPageIndex = -1;
    public Action<int> OnPageChanged;

    private bool stopMove = true;
    public float smooting = 4;                      //滑动速度  
    public float sensitivity = 0;
    private float startTime;
    private float startDragHorizontal;


    public bool Is_Auto;         // 是否自动展示
    public float Auto_Delay =1.5f;     // 自动展示延迟 



    void Awake () {
        rect = transform.GetComponent<ScrollRect> ();
        float horizontalLength = rect.content.rect.width - GetComponent<RectTransform> ().rect.width;
        posList.Add (0);
        for(int i = 1; i < rect.content.transform.childCount - 1; i++) {
            posList.Add (GetComponent<RectTransform> ().rect.width * i / horizontalLength);
        }
        posList.Add (1);


        Auto_Show();
    }

    void Update () {
        if(!isDrag && !stopMove) {
            startTime += Time.deltaTime;
            float t = startTime * smooting;
            rect.horizontalNormalizedPosition = Mathf.Lerp (rect.horizontalNormalizedPosition , targethorizontal , t);
            if(t >= 1)
                stopMove = true;
        }
    }

    public void pageTo (int index) {
        if(index >= 0 && index < posList.Count) {
            //rect.horizontalNormalizedPosition = posList[index];
            rect.DOHorizontalNormalizedPos(posList[index],0.3f);
            SetPageIndex(index);
        } else {   
            Debug.LogWarning ("页码不存在");
            Auto_i = 0;
        }
    }

    private void SetPageIndex (int index) {
        if(currentPageIndex != index) {
            currentPageIndex = index;
            if(OnPageChanged != null)
                OnPageChanged (index);
        }
    }

    public void OnBeginDrag (PointerEventData eventData) {
        isDrag = true;
        startDragHorizontal = rect.horizontalNormalizedPosition; 
    }
    
    public void OnEndDrag (PointerEventData eventData) {
        float posX = rect.horizontalNormalizedPosition;
        posX += ((posX - startDragHorizontal) * sensitivity);
        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;
        int index = 0;
        float offset = Mathf.Abs (posList[index] - posX);
        for(int i = 1; i < posList.Count; i++) {
            float temp = Mathf.Abs (posList[i] - posX);
            if(temp < offset) {
                index = i;
                offset = temp;
            }
        }
        SetPageIndex (index);

        targethorizontal = posList[index]; //设置当前坐标，更新函数进行插值  
        isDrag = false;
        startTime = 0;
        stopMove = false;
    }

    int Auto_i = 0;
    public void Auto_Show()
    {
        if (Is_Auto)
        {
            StartCoroutine(Auto_Next());
        }
    }

    IEnumerator Auto_Next()
    {
        yield return new WaitForSeconds(Auto_Delay);
        pageTo(Auto_i++);
        StartCoroutine(Auto_Next());
    }

}
