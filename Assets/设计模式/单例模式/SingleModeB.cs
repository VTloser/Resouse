using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleModeB : MonoBehaviour
{

    //无需挂载
    public int Propetry = 10;

    private static SingleModeB instacne;
    public static SingleModeB Instance
    {
        get
        {
            if (instacne == null)
                instacne = new SingleModeB();
            return instacne;
        }
    }


}
