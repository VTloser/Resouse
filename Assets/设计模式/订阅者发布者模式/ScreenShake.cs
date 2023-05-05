using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//订阅者

public class ScreenShake : MonoBehaviour
{
    public string Info; 

    void Start()
    {
        EventManager.Instance.OnplayerGetHit += Shake;
        EventManager.Instance.OnplayerGetHit_int += Shake;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnplayerGetHit -= Shake;
        EventManager.Instance.OnplayerGetHit_int += Shake;
    }

    public void Shake()
    {
        Debug.Log(Info);
    }


    public void Shake(int hp)
    {
        Debug.Log(Info + "HP:" + hp);
    }
}
