using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleModeA : MonoBehaviour
{
    public int Propetry = 100;

    public static SingleModeA Instance;


    private void Awake()
    {
        Instance = this;
    }
}
