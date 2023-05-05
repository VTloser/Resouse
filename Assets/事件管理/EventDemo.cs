using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(GameEvent.Event_Demo1, NoArage1);
        EventCenter.AddListener(GameEvent.Event_Demo1, NoArage2);
        EventCenter.AddListener(GameEvent.Event_Demo2, (string _) => { Debug.Log("123" + _); });
        EventCenter.AddListener(GameEvent.Event_Demo2, (string _) => { Debug.Log("456" + _); });
        EventCenter.AddListener(GameEvent.Event_Demo3, (string _, float i) => { Debug.Log("123" + _ + ":" + i); });

        EventCenter.Broadcast(GameEvent.Event_Demo1);
        EventCenter.Broadcast(GameEvent.Event_Demo2, "Hello");
        EventCenter.Broadcast(GameEvent.Event_Demo3, "Hello", 10f);


        EventCenter.RemoveListener(GameEvent.Event_Demo1, NoArage2);
        EventCenter.RemoveListener(GameEvent.Event_Demo2, (string _) => { Debug.Log("456" + _); });

        EventCenter.Broadcast(GameEvent.Event_Demo1);
        EventCenter.Broadcast(GameEvent.Event_Demo2, "Hello");


        EventCenter.ClearUp();

    }


    public void NoArage1()
    {
        Debug.Log("123");
    }

    public void NoArage2()
    {
        Debug.Log("456");
    }

}
