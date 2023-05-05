using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour, Item
{
    public Packing packing()
    {
        return new Wrapper();
    }

    public float Price()
    {
        return 0;
        throw new System.NotImplementedException();
    }

    string Item.name()
    {
        return "";
        throw new System.NotImplementedException(); 
    }
}
