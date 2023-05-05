using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 发布者
public class Players : MonoBehaviour
{

    private int _hp = 10;  // 属性

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnTakeHit();
        }
    }


    //当玩家受当伤害时，调用通知所有观察者
    public void OnTakeHit()
    {
        _hp--; //声明减少
        //收到攻击，调用通知
        //EventManager.Instance.DE();

        EventManager.Instance.DE(_hp);
    }

}
