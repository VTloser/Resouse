using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    void Start()
    {
        Debug.Log(SingleModeA.Instance.Propetry); //单例一
        Debug.Log(SingleModeB.Instance.Propetry); //单例二
        Debug.Log(SingleModeC.Instance.Propetry); //单例三
    }


}
