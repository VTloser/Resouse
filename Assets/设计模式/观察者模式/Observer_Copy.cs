using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Look
{
    public class Observer_Copy : MonoBehaviour, ITakeHitObserver
    {

        public Player player; //被观察者引用，来实现添加和移除观察者
        public string Info;

        private void Start()
        {
            player.AddObserver(this);
        }
        //移除绑定
        private void OnDisable()
        {
            player.RemoveObserver(this);
        }

        public void Notify()
        {
            Shake();
        }

        private void Shake()
        {
            Debug.Log(Info);
        }
    }
}