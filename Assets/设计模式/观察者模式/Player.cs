using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Look
{
    public class Player : MonoBehaviour
    {

        private int _hp = 10;  // 属性

        List<ITakeHitObserver> _takehitoberver;//观察者List

        private void Awake()
        {
            _takehitoberver = new List<ITakeHitObserver>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnTakeHit();
            }
        }

        //添加相关的增加和移除方法
        public void AddObserver(ITakeHitObserver observer)
        {
            _takehitoberver.Add(observer);
        }

        public void RemoveObserver(ITakeHitObserver observer)
        {
            _takehitoberver.Remove(observer);
        }


        //通知所有观察者方法
        private void NotifyObserver()
        {
            foreach (var item in _takehitoberver)
            {
                item.Notify();
            }
        }

        //当玩家受当伤害时，调用通知所有观察者
        public void OnTakeHit()
        {
            _hp--; //声明减少
                   //收到攻击，调用通知
            NotifyObserver();
        }

    }
}