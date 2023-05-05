using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class LaKuangMgr : MonoBehaviour
{
    /// <summary>    /// λ������    /// </summary>
    public Transform target;
    /// <summary>    /// ��Сֵ    /// </summary>
    public float MinPos;
    /// <summary>    /// �������    /// </summary>
    public float MaxPos;
    /// <summary>    /// λ�Ʋ�    /// </summary>
    public float displacement;
    /// <summary>    /// uiλ��״̬    /// </summary>
    private bool UIMoveState;
    /// <summary>    /// ����    /// </summary>
    public Button Right;
    /// <summary>    /// ����    /// </summary>
    public Button Left;
    /// <summary>    /// �仯����    /// </summary>
    public UIMove_axle uIMove_Axle;

    public enum UIMove_axle
    {
        X,
        Y,
    }

    /// <summary>    /// ����������Ǵ������¼�    /// </summary>
    private UnityAction rightAction;
    /// <summary>    /// ����������Ǵ������¼�    /// </summary>
    public void RightAction(UnityAction rightAction)
    {
        this.rightAction = rightAction;
    }
    /// <summary>    /// ���Ҳ������ʱ�򴥷����¼�    /// </summary>
    private UnityAction leftAction;
    /// <summary>    /// ���Ҳ������ʱ�򴥷����¼�    /// </summary>
    public void LeftAction(UnityAction leftAction)
    {
        this.leftAction = leftAction;
    }

    public void Start()
    {
        Right.onClick.AddListener(UIXRightMove);
        Left.onClick.AddListener(UIXLeftMove);
    }
    public void UIXRightMove()
    {
        UIXMove(displacement);
        Right.gameObject.SetActive(false);
        Left.gameObject.SetActive(true);
        rightAction?.Invoke();
    }
    public void UIXLeftMove()
    {
        UIXMove(-displacement);
        Right.gameObject.SetActive(true);
        Left.gameObject.SetActive(false);
        leftAction?.Invoke();
    }
    /// <summary>    /// UIλ��    /// </summary>
    /// <param name="MaxPos">���λ��</param>
    /// <param name="MinPos">��Сλ��</param>
    /// <param name="displacement">λ�Ʋ�ֵ</param>
    private void UIXMove(float mDisplacement)
    {
        if (UIMoveState)
            return;
        UIMoveState = true;
        float num = Mathf.Clamp(target.localPosition.x + mDisplacement, MinPos, MaxPos);

        target.DOLocalMoveX(num, 1).OnComplete(() =>
        {
            UIMoveState = false;
        }).SetEase(Ease.OutBack,2);
    }
    /// <summary>    /// uiλ��״̬    /// </summary>
    protected bool UIYMoveState;
    /// <summary>    /// UIλ��    /// </summary>
    /// <param name="MaxPos">���λ��</param>
    /// <param name="MinPos">��Сλ��</param>
    /// <param name="displacement">λ�Ʋ�ֵ</param>
    private void UIYMove(float mDisplacement)
    {
        if (UIYMoveState)
            return;
        UIYMoveState = true;
        float num = Mathf.Clamp(target.localPosition.y + mDisplacement, MinPos, MaxPos);

        target.DOLocalMoveY(num, 1).OnComplete(() =>
        {
            UIYMoveState = false;
        });
    }
}
