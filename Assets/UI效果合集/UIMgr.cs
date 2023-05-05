using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using Coffee.UIEffects;
using UnityEngine.EventSystems;

namespace YCUI
{
    public class Init : MonoBehaviour
    {
        public GameObject sidebar;       /// 侧边框
        public float Move_Distance;     /// 弹出距离
        bool Is_Popup = true ;          /// 状态是否弹出
        private void Start()
        {
            
        }

    }



    public static class UIMgr_Demo
    {

        #region UI出现

        /// <summary>
        /// UI出现
        /// </summary>
        /// <param name="image"></param>
        public static UIBehaviour UIAppear(this UIBehaviour image)
        {
            image.gameObject.SetActive(true);
            return image;
        }

        /// <summary>
        /// UI渐变大
        /// </summary>
        /// <param name="image" ></param>
        /// <param name="Time"  ></param>
        /// <param name="action"></param>
        public static void UIBigger(this UIBehaviour image, float Time = 0.5f, UnityAction action = null)
        {
            image.gameObject.SetActive(true);
            image.transform.localScale = Vector3.zero;
            image.transform.DOScale(Vector3.one, Time).OnComplete(() => { action?.Invoke(); });
        }

        /// <summary>
        /// UI Alpha渐入
        /// </summary>
        /// <param name="image" ></param>
        /// <param name="Time"  ></param>
        /// <param name="action"></param>
        public static void UIFadeIn(this UIBehaviour image, float Time = 1.5f, UnityAction action = null)
        {
            if (!image.gameObject.GetComponent<CanvasGroup>())
                image.gameObject.AddComponent<CanvasGroup>();
            image.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            image.gameObject.SetActive(true);
            float Alpha = 0;
            DOTween.To(() => 0f, x => Alpha = x, 1f, Time)
                .OnUpdate(() =>
                {
                    image.gameObject.GetComponent<CanvasGroup>().alpha = Alpha;
                })
                .OnComplete(() =>
                {
                    //MonoBehaviour.Destroy(image.gameObject.GetComponent<CanvasGroup>());
                    action?.Invoke();
                });
        }
        /// <summary>
        /// UI花式渐入  注:Toggle和Slider无法使用
        /// </summary>
        /// <param name="image" ></param>
        /// <param name="Time"  ></param>
        /// <param name="action"></param>

        public static void UI_BeaIn(this UIBehaviour image, float Time = 1.5f, UnityAction action = null)
        {
            image.gameObject.SetActive(true);
            if (!image.gameObject.GetComponent<UITransitionEffect>())
                image.gameObject.AddComponent<UITransitionEffect>();
            image.gameObject.GetComponent<UITransitionEffect>().effectFactor          = 0;
            image.gameObject.GetComponent<UITransitionEffect>().effectMode            = UITransitionEffect.EffectMode.Dissolve;
            image.gameObject.GetComponent<UITransitionEffect>().transitionTexture     = Resources.Load<Texture>("UITransitionTex");
            image.gameObject.GetComponent<UITransitionEffect>().effectPlayer.duration = Time;
            image.gameObject.GetComponent<UITransitionEffect>().Show();
        }

        #endregion

        #region UI消失
        /// <summary>
        /// UI消失
        /// </summary>
        /// <param name="image"></param>
        public static UIBehaviour UIDisAppear(this UIBehaviour image)
        {
            image.gameObject.SetActive(false);
            return image;
        }

        /// <summary>
        /// UI渐变小
        /// </summary>
        /// <param name="image" ></param>
        /// <param name="Time"  ></param>
        /// <param name="action"></param>
        public static void UISmaller(this UIBehaviour image, float Time = 0.5f, UnityAction action = null)
        {
            image.gameObject.SetActive(true);
            image.transform.localScale = Vector3.one;
            image.transform.DOScale(Vector3.zero, Time).OnComplete(() =>
             {
                 image.gameObject.SetActive(false);
                 action?.Invoke();
             });
        }


        /// <summary>
        /// UI Alpha渐出
        /// </summary>
        /// <param name="image" ></param>
        /// <param name="Time"  ></param>
        /// <param name="action"></param>
        public static void UIFadeOut(this UIBehaviour image, float Time = 1.5f, UnityAction action = null)
        {
            if (!image.gameObject.GetComponent<CanvasGroup>())
                image.gameObject.AddComponent<CanvasGroup>();
            image.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            image.gameObject.SetActive(true);
            float Alpha = 0;
            DOTween.To(() => 1f, x => Alpha = x, 0f, Time)
                .OnUpdate(() =>
                {
                    image.gameObject.GetComponent<CanvasGroup>().alpha = Alpha;
                })
                .OnComplete(() =>
                {
                    MonoBehaviour.Destroy(image.gameObject.GetComponent<CanvasGroup>());
                    image.gameObject.SetActive(false);
                    action?.Invoke();
                });
        }

        /// <summary>
        /// UI花式渐出 注:Toggle和Slider无法使用
        /// </summary>
        /// <param name="image" ></param>
        /// <param name="Time"  ></param>
        /// <param name="action"></param>

        public static void UI_BeaOut(this UIBehaviour image, float Time = 1.5f, UnityAction action = null)
        {
            image.gameObject.SetActive(true);
            if (!image.gameObject.GetComponent<UITransitionEffect>())
                image.gameObject.AddComponent<UITransitionEffect>();
            image.gameObject.GetComponent<UITransitionEffect>().effectFactor          = 1;
            image.gameObject.GetComponent<UITransitionEffect>().effectMode            = UITransitionEffect.EffectMode.Dissolve;
            image.gameObject.GetComponent<UITransitionEffect>().transitionTexture     = Resources.Load<Texture>("UITransitionTex");
            image.gameObject.GetComponent<UITransitionEffect>().effectPlayer.duration = Time;
            image.gameObject.GetComponent<UITransitionEffect>().Hide();
        }

        /// <summary>
        /// UI关闭
        /// </summary>
        /// <param name="image" ></param>
        /// <param name="Time"  ></param>
        /// <param name="action"></param>
        public static void UI_TVClose(this UIBehaviour image, float Time = 0.75f, UnityAction action = null)
        {
            image.gameObject.SetActive(true);

            float width  = image.gameObject.GetComponent<RectTransform>().sizeDelta.x;
            float height = image.gameObject.GetComponent<RectTransform>().sizeDelta.y;

            float Temp = 0;
            DOTween.To(() => width, X => Temp = X, width * 2, Time).OnUpdate(() => { image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Temp, image.gameObject.GetComponent<RectTransform>().sizeDelta.y); });
            DOTween.To(() => height, X => Temp = X, 0, Time)
                .OnUpdate(() =>
                {
                    image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(image.gameObject.GetComponent<RectTransform>().sizeDelta.x, Temp);
                })
                .OnComplete(() =>
                {
                    action?.Invoke();
                });
                //.SetEase(Ease.Linear);
        }

        #endregion
    }

    /// <summary>
    /// 测试类
    /// </summary>
    public class UIMgr : MonoBehaviour
    {

        public Dropdown A;
        public Button B;
        public Toggle C;
        public Slider D;
        public Image E;
        public Text F;


        private void Start()
        {
            GetComponentInChildren<Button>().onClick.AddListener(() => { Debug.Log(123); });
        }

        private void OnGUI()
        {
            if (GUILayout.Button("AAAAAAAAA"))
            {
                A.UI_TVClose();
                B.UI_TVClose();
                C.UI_TVClose();
                D.UI_TVClose();
                E.UI_TVClose();
                F.UI_TVClose();
            }
        }
    }
}

