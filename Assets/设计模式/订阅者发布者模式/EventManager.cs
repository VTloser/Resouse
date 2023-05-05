using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//事件管理器  

public class EventManager 
{
    public static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new EventManager();
            return _instance;
        }
    }

    public event Action  OnplayerGetHit;  //所有玩家受到攻击后需要调用的方法
    public event Action<int> OnplayerGetHit_int;  //所有玩家受到攻击后需要调用的方法
    // 给外部调用的方法，来通知事件中心派发事件p
    public void DE()
    {
        OnplayerGetHit?.Invoke();
    }

    public void DE(int hp)
    {
        OnplayerGetHit_int?.Invoke(hp);
    }
}
