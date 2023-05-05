using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameEvent : int
{
    Event_Error = -1,
    Event_None = 0,

    Event_Demo1,
    Event_Demo2,
    Event_Demo3,
}


public delegate void EventCallback();
public delegate void EventCallback<T>(T t);
public delegate void EventCallback<T, U>(T t, U u);
public delegate void EventCallback<T, U, V>(T t, U u, V v);
public delegate void EventCallback<T, U, V, X>(T t, U u, V v, X x);