using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleModeC : Singleton<SingleModeC>
{
    //直接继承 Singleton<SingleModeC>  则直接成为单例
    public int Propetry = 666;

}
