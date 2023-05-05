using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class LaKuangMgr : MonoBehaviour
{
    /// <summary>    /// 位移物体    /// </summary>
    public Transform target;
    /// <summary>    /// 最小值    /// </summary>
    public float MinPos;
    /// <summary>    /// 最大限制    /// </summary>
    public float MaxPos;
    /// <summary>    /// 位移差    /// </summary>
    public float displacement;
    /// <summary>    /// ui位移状态    /// </summary>
    private bool UIMoveState;
    /// <summary>    /// 右手    /// </summary>
    public Button Right;
    /// <summary>    /// 左手    /// </summary>
    public Button Left;
    /// <summary>    /// 变化次数    /// </summary>
    public UIMove_axle uIMove_Axle;

    public enum UIMove_axle
    {
        X,
        Y,
    }

    /// <summary>    /// 往左侧拉框是触发的事件    /// </summary>
    private UnityAction rightAction;
    /// <summary>    /// 往左侧拉框是触发的事件    /// </summary>
    public void RightAction(UnityAction rightAction)
    {
        this.rightAction = rightAction;
    }
    /// <summary>    /// 往右侧拉伸的时候触发的事件    /// </summary>
    private UnityAction leftAction;
    /// <summary>    /// 往右侧拉伸的时候触发的事件    /// </summary>
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
    /// <summary>    /// UI位移    /// </summary>
    /// <param name="MaxPos">最大位移</param>
    /// <param name="MinPos">最小位移</param>
    /// <param name="displacement">位移差值</param>
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
    /// <summary>    /// ui位移状态    /// </summary>
    protected bool UIYMoveState;
    /// <summary>    /// UI位移    /// </summary>
    /// <param name="MaxPos">最大位移</param>
    /// <param name="MinPos">最小位移</param>
    /// <param name="displacement">位移差值</param>
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
