/***
*	Title："三维可视化" 项目
*		主题：手势控制物体的旋转缩放
*	Description：
*		功能：
*                    1、实现单指滑动左右移动镜头
*                    2、双指缩放镜头           
*	Date：2019
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class Control : MonoBehaviour
    {
        //摄像机距离
        float distance = 60f;
        //缩放系数
        float scaleFactor = 1f;


        float maxDistance = 75f;
        float minDistance = 45f;


        //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
        private Vector2 oldPosition1;
        private Vector2 oldPosition2;


        private Vector2 lastSingleTouchPosition;

        private Vector3 m_CameraOffset;
        private static Camera m_Camera;

        public bool useMouse = true;
        public bool lookObj = false;

        //定义摄像机可以活动的范围
        public float xMin = -100;
        public float xMax = 100;
        public float zMin = -100;
        public float zMax = 100;

        //这个变量用来记录单指双指的变换
        private bool m_IsSingleFinger;

        //初始化游戏信息设置
        void Start()
        {
            m_Camera = Camera.main;
        }



        void Update()
        {
            if (!Be_Rocker_Le && !Be_Rocker_Ri)
            {
                if (Input.touchCount > 1)
                {
                    //当从单指触摸进入多指触摸的时候,记录一下触摸的位置
                    //保证计算缩放都是从两指手指触碰开始的
                    if (m_IsSingleFinger)
                    {
                        oldPosition1 = Input.GetTouch(0).position;
                        oldPosition2 = Input.GetTouch(1).position;
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                    {
                        ScaleCamera();
                    }
                    m_IsSingleFinger = false;
                    //Handheld.Vibrate();//调用手机震动
                    Debug.Log(123);
                }
                if (Input.touchCount <= 1)
                {
                    m_IsSingleFinger = true;
                }
            }

        }

        /// <summary>
        /// 触摸缩放摄像头
        /// </summary>
        private void ScaleCamera()
        {
            //计算出当前两点触摸点的位置
            var tempPosition1 = Input.GetTouch(0).position;
            var tempPosition2 = Input.GetTouch(1).position;

            float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
            float lastTouchDistance    = Vector3.Distance(oldPosition1 , oldPosition2 );

            //计算上次和这次双指触摸之间的距离差距
            //然后去更改摄像机的距离
            distance -= (currentTouchDistance - lastTouchDistance) * scaleFactor * Time.deltaTime;

            //把距离限制住在min和max之间
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            m_Camera.fieldOfView = distance;

            //备份上一次触摸点的位置，用于对比
            oldPosition1 = tempPosition1;
            oldPosition2 = tempPosition2;
        }


        bool Be_Rocker_Le;
        public void In_Rocker_Left()
        {
            Be_Rocker_Le = true;
            Debug.Log("在控制中");
        }
        public void Over_Rocker_Left()
        {
            Be_Rocker_Le = false;
            Debug.Log("结束控制");
        }

        bool Be_Rocker_Ri;
        public void In_Rocker_Right()
        {
            Be_Rocker_Ri = true;
            Debug.Log("在控制中");
        }
        public void Over_Rocker_Right()
        {
            Be_Rocker_Ri = false;
            Debug.Log("结束控制");
        }



    }

}
